using Microsoft.EntityFrameworkCore;
using OrbitalGuardApi.Models;

namespace OrbitalGuardApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Sensor> Sensores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurando explicitamente o relacionamento 1:N
            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.Cidade)
                .WithMany(c => c.Sensores)
                .HasForeignKey(s => s.CidadeId)
                .OnDelete(DeleteBehavior.Cascade); // Excluir cidade apaga os sensores dela automaticamente

            base.OnModelCreating(modelBuilder);
        }
    }
}