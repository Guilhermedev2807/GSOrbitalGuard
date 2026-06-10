namespace OrbitalGuardApi.Models
{
    public class Cidade
    {
        // Deixe apenas a linha abaixo, SEM a anotação em cima:
        public long Id { get; set; }

        public string? Nome { get; set; }
        public string? Estado { get; set; }
        public string? RiscoAtual { get; set; }
        public ICollection<Sensor>? Sensores { get; set; }
    }
}