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
            base.OnModelCreating(modelBuilder);

            // 1. Mapeamento Oficial da Cidade
            modelBuilder.Entity<Cidade>(entity =>
            {
                entity.ToTable("T_OG_CIDADE", "RM566087");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID_CIDADE");
                entity.Property(e => e.Nome).HasColumnName("NM_CIDADE");
                entity.Property(e => e.Estado).HasColumnName("SG_ESTADO");
                entity.Property(e => e.RiscoAtual).HasColumnName("NR_RISCO_ATUAL");
            });

            // 2. Mapeamento Oficial do Sensor (Corrigido e Blindado)
           // 2. Mapeamento Oficial do Sensor
            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("T_OG_SENSOR", "RM566087");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID_SENSOR");
                entity.Property(e => e.Tipo).HasColumnName("TP_SENSOR");
                entity.Property(e => e.Localizacao).HasColumnName("DS_LOCALIZACAO");
                
                // ATUALIZA ESTA LINHA: Mapeia o Status diretamente para a coluna que o Oracle exige
                entity.Property(e => e.Status).HasColumnName("ST_SENSOR"); 
                
                entity.Property(e => e.CidadeId).HasColumnName("ID_CIDADE");

                entity.HasOne(e => e.Cidade)
                      .WithMany()
                      .HasForeignKey(e => e.CidadeId);
            });
        }
    }
}