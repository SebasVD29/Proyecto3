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
        
        DataBaseController dataBase = new DataBaseController();

        // GET: api/<ClientesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
         public void Get(int id)
         {
             
             try
             {
                 dataBase.StringConexion().Open();
                 string querySQL = "SELECT * FROM dbo.Clientes WHERE IdentificadorCliente = @id";

                 using (SqlCommand command = new SqlCommand(querySQL, dataBase.StringConexion()))
                 {
                     command.Parameters.AddWithValue("@id", id);
                     using (SqlDataReader reader = command.ExecuteReader())
                     {
                         while(reader.Read())
                         {
                            
                         }
                         reader.Close();
                     }
                 }
                dataBase.StringConexion().Close();
                
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
                 throw;
             }
         }
        
        // POST api/<ClientesController>
        [HttpPost]
        public void Post(int identificadorCliente, string nombreCompleto, int telefono, string direccion, string email, int estado)
        {
            
            try
            {
                dataBase.StringConexion().Open();
                string querySQL = "INSERT INTO dbo.Clientes(IdentificadorCliente, NombreCompleto, Direccion, Telefono, Email, Estado) " +
                                  "VALUES (@identificacion, @nombreCompleto,@direccion, @telefono, @email, @estado)";

                using (SqlCommand command = new SqlCommand(querySQL, dataBase.StringConexion()))
                {
                    command.Parameters.AddWithValue("@identificacion", identificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@direccion",direccion);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.ExecuteNonQuery();
                }
                dataBase.StringConexion().Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return;
        }

        // PUT api/<ClientesController>/5  (hacer)
        [HttpPut("{id}")]
        public void Put(int identificadorCliente, string nombreCompleto, int telefono, string direccion, string email, string estado)
        {

            

            try
            {
                dataBase.StringConexion().Open();
                string querySQL = "UPDATE dbo.Clientes SET NombreCompleto = @nombreCompleto, Telefono = @telefono, Direccion = @direccion, Email = @email, Estado = @estado " +
                                  "WHERE Identificacion = @identificadorCliente";

                using (SqlCommand command = new SqlCommand(querySQL, dataBase.StringConexion()))
                {
                    command.Parameters.AddWithValue("@identificadorCliente",identificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.ExecuteNonQuery();
                }
                dataBase.StringConexion().Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return; 
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Aquí puedes agregar el código para eliminar un cliente basado en su identificación.
            return Ok("Cliente eliminado exitosamente.");
        }
    }
}
