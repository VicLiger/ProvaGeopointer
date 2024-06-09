using Dapper;
using EquipamentoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Linq.Expressions;

namespace EquipamentoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipamentoController : ControllerBase
    {

        public readonly string _connectionString;
        public EquipamentoController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEquipaments()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    const string query = "SELECT * FROM equipamentos";

                    var equipaments = await conn.QueryAsync<EquipamentoModel>(query);

                    if (equipaments is null || equipaments.Count().Equals(0))
                        throw new InvalidOperationException("Não achamos nenhum equipamento");

                    return Ok(equipaments);

                }
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);

            }
            catch (NpgsqlException ex)
            {
                return BadRequest("Ocorreu um erro ao acessar ro banco de dados");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
