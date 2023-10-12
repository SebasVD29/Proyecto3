using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

namespace API_Clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class ClientesController : ControllerBase
    {
        string connectionString = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        SqlConnection? sqlConnection;

        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            // Aquí puedes agregar el código para obtener todos los clientes si es necesario.
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string querySQL = "SELECT * FROM dbo.Clientes WHERE IdentificadorCliente = @id";

                using (SqlCommand command = new SqlCommand(querySQL, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Ok(new
                            {
                                identificacion = reader["IdentificadorCliente"],
                                nombreCompleto = reader["NombreCompleto"],
                                direccion = reader["Direccion"],
                                telefono = reader["Telefono"],
                                email = reader["Email"],
                                estado = reader["Estado"]
                            });
                        }
                        reader.Close();
                    }
                }
                return NotFound("Cliente no encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        public void Post(int identificadorCliente, string nombreCompleto, int telefono, string direccion, string email, string estado)
        {
            sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string querySQL = "INSERT INTO dbo.Clientes(IdentificadorCliente, NombreCompleto, Direccion, Telefono, Email, Estado) " +
                                  "VALUES (@identificacion, @nombreCompleto, @telefono, @direccion, @email, @estado)";

                using (SqlCommand command = new SqlCommand(querySQL, sqlConnection))
                {
                    command.Parameters.AddWithValue("@identificacion", cliente.IdentificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", cliente.NombreCompleto);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@email", cliente.Email);
                    command.Parameters.AddWithValue("@estado", cliente.Estado);
                    command.ExecuteNonQuery();
                }
                return Ok("Cliente agregado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // PUT api/<ClientesController>/5  (hacer)
        [HttpPut("{id}")]
        public IActionResult Put(int id [FromBody] Cliente cliente)
        {
            sqlConnection = new SqlConnection(connectionString);
,
            try
            {
                sqlConnection.Open();
                string querySQL = "UPDATE dbo.Clientes SET NombreCompleto = @nombreCompleto, Telefono = @telefono, Direccion = @direccion, Email = @email, Estado = @estado " +
                                  "WHERE Identificacion = @identificacion";

                using (SqlCommand command = new SqlCommand(querySQL, sqlConnection))
                {
                    command.Parameters.AddWithValue("@identificadorCliente", cliente.IdentificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", cliente.NombreCompleto);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@email", cliente.Email);
                    command.Parameters.AddWithValue("@estado", cliente.Estado);
                    command.ExecuteNonQuery();
                }
                return Ok("Cliente actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Aquí puedes agregar el código para eliminar un cliente basado en su identificación.
            return Ok("Cliente eliminado exitosamente.");
        }
    }

    public class Cliente
    {
        public int IdentificadorCliente { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
    }
}
