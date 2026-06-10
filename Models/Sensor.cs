using OrbitalGuardApi.Models;

namespace OrbitalGuardApi.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public string? Status { get; set; }
        
        // 👈 ADICIONE ESTA LINHA ABAIXO QUE ESTAVA FALTANDO:
        public string? Localizacao { get; set; } 

        // Chave Estrangeira correta (long) que conversa com a Cidade
        public long CidadeId { get; set; } 
        
        // Propriedade de Navegação ÚNICA
        public Cidade? Cidade { get; set; } 
    }
}