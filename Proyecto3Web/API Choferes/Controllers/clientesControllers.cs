﻿using API_Choferes.Controllers;
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
                this.conexion.Open();
                 string querySQL = "SELECT * FROM dbo.Clientes WHERE IdentificadorCliente = @id";

                 using (SqlCommand command = new SqlCommand(querySQL, this.conexion))
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
                this.conexion.Close();
                
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
                this.conexion.Open();
                string querySQL = "INSERT INTO dbo.Clientes(IdentificadorCliente, NombreCompleto, Direccion, Telefono, Email, Estado) " +
                                  "VALUES (@identificacion, @nombreCompleto,@direccion, @telefono, @email, @estado)";

                using (SqlCommand command = new SqlCommand(querySQL, this.conexion))
                {
                    command.Parameters.AddWithValue("@identificacion", identificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@direccion",direccion);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.ExecuteNonQuery();
                }
                this.conexion.Close();
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
                this.conexion.Open();
                string querySQL = "UPDATE dbo.Clientes SET NombreCompleto = @nombreCompleto, Direccion = @direccion, Telefono = @telefono, Email = @email, Estado = @estado " +
                                  "WHERE Identificacion = @identificadorCliente";

                using (SqlCommand command = new SqlCommand(querySQL, this.conexion))
                {
                    command.Parameters.AddWithValue("@identificadorCliente",identificadorCliente);
                    command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.ExecuteNonQuery();
                }
                this.conexion.Close();

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