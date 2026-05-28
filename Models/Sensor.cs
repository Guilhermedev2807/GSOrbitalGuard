using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrbitalGuardApi.Models
{
    [Table("TB_OG_SENSOR")]
    public class Sensor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty; // "Pluviômetro", "Nível de Água"

        [Required]
        [StringLength(100)]
        public string Localizacao { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Ativo";

        // Chave Estrangeira para Cidade
        [Required]
        public int CidadeId { get; set; }

        [ForeignKey("CidadeId")]
        [JsonIgnore] // Evita loop infinito na serialização do JSON
        public Cidade? Cidade { get; set; }
    }
}