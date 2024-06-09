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
                return BadRequest("Ocorreu um erro ao acessar o banco de dados");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddEquipaments(EquipamentoModel equipament)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO equipamentos (Tag, Name, File, State)
                        VALUES (@Tag, @Name, @File, @State)
                        RETURNING id"; // Retorna o ID do equipamento inserido

                    await conn.ExecuteAsync(query, equipament);

                    return Ok(equipament);
                }
            }
            catch (NpgsqlException ex)
            {
                return BadRequest("Ocorreu um erro ao acessar o banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipament(int id)
        {


            try
            {

                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM equipamentos WHERE id = @id";

                    int remove = await conn.ExecuteAsync(query, id);

                    return Ok($"Item com o id = {id} removido com sucesso");

                }

            }
            catch (NpgsqlException ex)
            {
                return BadRequest("Ocorreu um erro ao acessar o banco de dados");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }



        [HttpPut]
        public async Task<IActionResult> UpdateEquipament(EquipamentoModel equipament)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "UPDATE equipamentos SET tag = @Tag, name = @Name, file = @File, state = @State WHERE id = @Id";
                    await conn.ExecuteAsync(query, equipament);

                    return Ok("Objeto atualizado: " + equipament);
                }
            }
            catch (NpgsqlException ex)
            {
                return BadRequest("Ocorreu um erro ao acessar o banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }



    }
}
