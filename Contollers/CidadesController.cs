using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitalGuardApi.Models;
using OrbitalGuardApi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // ==========================================
        // 1. READ ALL (Buscar Todas as Cidades)
        // ==========================================
        // ==========================================
// 1. READ ALL (Buscar Todas as Cidades)
// ==========================================
[HttpGet]
public async Task<ActionResult<IEnumerable<Cidade>>> GetCidades()
{
    // Mudamos para os nomes reais das colunas que o Entity Framework espera receber
    var sql = "SELECT ID_CIDADE, NM_CIDADE, SG_ESTADO, NR_RISCO_ATUAL FROM TB_OG_CIDADE";
    return await _context.Cidades.FromSqlRaw(sql).ToListAsync();
}

        // ==========================================
        // 2. READ BY ID (Buscar Cidade por ID)
        // ==========================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Cidade>> GetCidade(long id)
        {
            Cidade? cidade = null; // 👈 Protegido com '?' contra erro de nulo

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT ID_CIDADE, NM_CIDADE, SG_ESTADO, NR_RISCO_ATUAL FROM TB_OG_CIDADE WHERE ID_CIDADE = :id";
                
                var parameter = command.CreateParameter();
                parameter.ParameterName = "id";
                parameter.Value = id;
                command.Parameters.Add(parameter);

                await _context.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        cidade = new Cidade
                        {
                            Id = reader.GetInt64(0),
                            Nome = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Estado = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            RiscoAtual = reader.IsDBNull(3) ? "" : reader.GetString(3)
                        };
                    }
                }
            }

            if (cidade == null)
            {
                return NotFound($"Cidade com o ID {id} não foi encontrada.");
            }

            return Ok(cidade);
        }

        // ==========================================
        // 3. CREATE (Criar Nova Cidade - POST)
        // ==========================================
        [HttpPost]
        public async Task<ActionResult<Cidade>> PostCidade(Cidade cidade)
        {
            var rand = new Random();
            cidade.Id = (long)rand.Next(10000000, 999999999);

            var sql = "INSERT INTO TB_OG_CIDADE (ID_CIDADE, NM_CIDADE, SG_ESTADO, NR_RISCO_ATUAL) VALUES ({0}, {1}, {2}, {3})";

            try
            {
                var nome = cidade.Nome ?? "";
                var estado = cidade.Estado ?? "";
                var risco = cidade.RiscoAtual ?? "";

                await _context.Database.ExecuteSqlRawAsync(sql, cidade.Id, nome, estado, risco);

                return CreatedAtAction("GetCidade", new { id = cidade.Id }, cidade);
            }
            catch (Exception ex)
            {
                var erroInterno = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest($"Erro ao salvar no Oracle: {erroInterno}");
            }
        }

        // ==========================================
        // 4. UPDATE (Atualizar Cidade - PUT)
        // ==========================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCidade(long id, Cidade cidade)
        {
            if (id != cidade.Id)
            {
                return BadRequest("O ID da URL não corresponde ao ID do objeto enviado.");
            }

            var sql = "UPDATE TB_OG_CIDADE SET NM_CIDADE = {0}, SG_ESTADO = {1}, NR_RISCO_ATUAL = {2} WHERE ID_CIDADE = {3}";

            try
            {
                var nome = cidade.Nome ?? "";
                var estado = cidade.Estado ?? "";
                var risco = cidade.RiscoAtual ?? "";

                int linhasAfetadas = await _context.Database.ExecuteSqlRawAsync(sql, nome, estado, risco, id);

                if (linhasAfetadas == 0)
                {
                    return NotFound($"Cidade com o ID {id} não foi encontrada.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar: {ex.Message}");
            }
        }

        // ==========================================
        // 5. DELETE (Apagar Cidade - DELETE)
        // ==========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade(long id)
        {
            var sql = "DELETE FROM TB_OG_CIDADE WHERE ID_CIDADE = {0}";

            try
            {
                int linhasAfetadas = await _context.Database.ExecuteSqlRawAsync(sql, id);

                if (linhasAfetadas == 0)
                {
                    return NotFound($"Cidade com o ID {id} não foi encontrada.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar: {ex.Message}");
            }
        }
    }
}