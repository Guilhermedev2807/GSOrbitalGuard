using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitalGuardApi.Models
{
    [Table("TB_OG_CIDADE")]
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(2)]
        public string Estado { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string RiscoAtual { get; set; } = "Baixo"; // Baixo, Médio, Alto

        // Relacionamento 1:N - Uma cidade tem muitos sensores
        public ICollection<Sensor> Sensores { get; set; } = new List<Sensor>();
    }
}