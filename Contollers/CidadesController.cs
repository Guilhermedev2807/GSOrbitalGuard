using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitalGuardApi.Data;
using OrbitalGuardApi.Models;

namespace OrbitalGuardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CidadesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cidade>>> GetCidades()
        {
            // Traz as cidades junto com a lista de sensores internos (.Include)
            return await _context.Cidades.Include(c => c.Sensores).ToListAsync();
        }

        // POST: api/Cidades
        [HttpPost]
        public async Task<ActionResult<Cidade>> PostCidade(Cidade cidade)
        {
            _context.Cidades.Add(cidade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCidades), new { id = cidade.Id }, cidade);
        }

        // DELETE: api/Cidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade(int id)
        {
            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null) return NotFound(new { message = "Cidade não encontrada" });

            _context.Cidades.Remove(cidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}