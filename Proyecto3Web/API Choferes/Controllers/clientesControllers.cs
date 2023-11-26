using API_Choferes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

namespace API_Clientes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class clientesControllers : ControllerBase
    {

        private DataBaseController dataBase;
        private SqlConnection conexion;

        public clientesControllers()
        {
            this.dataBase = new DataBaseController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            Console.WriteLine(id);
            string telefono = "";
            string estado = "";
            try
            {
                this.conexion.Open();
                string querySQL = "SELECT * FROM dbo.Clientes WHERE IdentificadorCliente = @id";
                using (SqlCommand command = new SqlCommand(querySQL, this.conexion))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {                 
                            if ((int)reader["Estado"] == 1)
                            {
                                estado = "Activo";
                            }
                            else
                            {
                                estado = "Inactivo";
                            }
                            telefono = Convert.ToString(reader["Telefono"]);
                        }
                        return Ok(new string[]
                        {
                            (string)reader["NombreCompleto"],
                            (string)reader["Direccion"],
                            telefono,
                            (string)reader["Email"],
                            estado
                        });

                        reader.Close();
                    }
                }
                this.conexion.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en la BD del select. {sqlEx.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = sqlEx.Message });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general del select. {ex.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = ex.Message });
            }
        }


        // POST api/<ClientesController>
        [HttpPost]
        public IActionResult Post(int identificadorCliente, string nombreCompleto, int telefono, string direccion, string email, int estado)
        {
            try
            {
                this.conexion.Open();
                string querySQL = "INSERT INTO dbo.Clientes(IdentificadorCliente, NombreCompleto, Direccion, Telefono, Email, Estado) " +
                                  "VALUES (@identificacion, @nombreCompleto,@direccion, @telefono, @email, @estado)";

                using (SqlCommand command = new SqlCommand(querySQL, this.conexion))
                {
                    command.Parameters.AddWithValue("@identificacion", identificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.ExecuteNonQuery();
                }
                this.conexion.Close();
                return Ok(new { Message = "Operación exitosa" });
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en la BD del insert. {sqlEx.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = sqlEx.Message });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general del insert. {ex.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = ex.Message });
            }
        }

        // PUT api/<ClientesController>/5  
        [HttpPut("{id}")]
        public IActionResult Put(int identificadorCliente, string nombreCompleto, int telefono, string direccion, string email, string estado)
        {
            try
            {
                this.conexion.Open();
                string querySQL = "UPDATE dbo.Clientes SET NombreCompleto = @nombreCompleto, Direccion = @direccion, Telefono = @telefono, Email = @email, Estado = @estado " +
                                  "WHERE IdentificadorCliente = @identificadorCliente";

                using (SqlCommand command = new SqlCommand(querySQL, this.conexion))
                {
                    command.Parameters.AddWithValue("@identificadorCliente", identificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.ExecuteNonQuery();
                }
                this.conexion.Close();
                return Ok(new { Message = "Operación exitosa" });

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en la BD del update. {sqlEx.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = sqlEx.Message });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general del update. {ex.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = ex.Message });
            }
        }
    }
}
