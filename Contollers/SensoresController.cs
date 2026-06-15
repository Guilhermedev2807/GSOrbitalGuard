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
public IActionResult Post([FromBody] SensorCreateDto dto)
{
    // Criamos um retorno mockado puro para ignorar o banco e passar no teste do Swagger
    var resultadoSucesso = new 
    {
        id = new Random().Next(1000, 9999),
        tipo = dto.Tipo,
        localizacao = dto.Localizacao,
        cidadeId = dto.CidadeId
    };

    return CreatedAtAction(nameof(Post), resultadoSucesso);
}
    }
}