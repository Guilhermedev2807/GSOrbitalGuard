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
public async Task<ActionResult<Sensor>> PostSensor(SensorCreateDto dto)
{
    // Buscando a cidade usando o ID convertido explicitamente para long
    var cidade = await _context.Cidades.FindAsync((long)dto.CidadeId);

    if (cidade == null)
    {
        return NotFound($"Cidade com o ID {dto.CidadeId} não foi encontrada.");
    }

    var sensor = new Sensor
    {
        Tipo = dto.Tipo,
        Localizacao = dto.Localizacao,
        CidadeId = dto.CidadeId // Certifique-se de que na model Sensor o CidadeId também seja long
    };

    _context.Sensores.Add(sensor);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetSensor", new { id = sensor.Id }, sensor);
}
    }
}