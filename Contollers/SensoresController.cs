using Microsoft.AspNetCore.Mvc;
using OrbitalGuardApi.Data;
using OrbitalGuardApi.DTOs;
using OrbitalGuardApi.Models;

namespace OrbitalGuardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensoresController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Sensores
        [HttpPost]
        public async Task<IActionResult> PostSensor([FromBody] SensorCreateDto dto)
        {
            // Validação automática disparada pelo [ApiController] usando DataAnnotations do DTO
            var cidadeExiste = await _context.Cidades.FindAsync(dto.CidadeId);
            if (cidadeExiste == null)
            {
                return BadRequest(new { error = "A cidade informada para o sensor não existe." });
            }

            var novoSensor = new Sensor
            {
                Tipo = dto.Tipo,
                Localizacao = dto.Localizacao,
                CidadeId = dto.CidadeId,
                Status = "Ativo"
            };

            _context.Sensores.Add(novoSensor);
            await _context.SaveChangesAsync();

            return Created("", novoSensor);
        }
    }
}