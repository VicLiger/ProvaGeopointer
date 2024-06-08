using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace ApiEquipament.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipamentController : ControllerBase
    {
        private readonly string _connectionString;
        public EquipamentController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conect");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {

                connection.Open();

                try
                {

                    const string query = "SELECT * FROM equipaments ";

                    var equipaments = await connection.QueryAsync(query);

                    return Ok(equipaments);

                } catch (NpgsqlException ex)
                { 
                    return BadRequest(ex.Message);

                } catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
