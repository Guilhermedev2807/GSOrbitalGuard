using System.ComponentModel.DataAnnotations;

namespace OrbitalGuardApi.DTOs
{
    public class SensorCreateDto
    {
        [Required(ErrorMessage = "O tipo do sensor é obrigatório.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A localização do sensor é obrigatória.")]
        public string Localizacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ID da cidade vinculada é obrigatório.")]
        public long CidadeId { get; set; }
    }
}