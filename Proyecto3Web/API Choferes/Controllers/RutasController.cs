using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Choferes.Controllers
{
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    [ApiController]
    public class RutasController : ControllerBase
    {
        private securityController securityController;
        private DataBaseController dataBase;
        private SqlConnection conexion;

        public RutasController()
        {
            this.dataBase = new DataBaseController();
            this.securityController = new securityController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }


        // Get rutas por cliente
        // GET api/<RutasController>/5
        [HttpGet("{id}")]
        public IActionResult ObtenerDireccionRuta(int id)
        {
            try
            {
                this.conexion.Open();
                string querySQL = "SELECT DireccionRuta.idDireccionRuta, DireccionRuta.NombreDireccionRuta, " +
                    "Pais.Nombre as NombrePais, Ciudad.Nombre as NombreCiudad " +
                    "FROM[dbo].DireccionRuta " +
                    "INNER JOIN[dbo].ClientePais ON DireccionRuta.IdClientePais = ClientePais.IdClientePais " +
                    "INNER JOIN[dbo].PaisCiudad ON DireccionRuta.IdPaisCiudad = PaisCiudad.IdPaisCiudad " +
                    "INNER JOIN[dbo].Pais ON Pais.IdentificadorPais = PaisCiudad.IdPais " +
                    "INNER JOIN[dbo].Ciudad ON Ciudad.IdentificadorCiudad = PaisCiudad.IdCiudad " +
                    "INNER JOIN[dbo].Clientes ON Clientes.IdentificadorCliente = ClientePais.IdCliente " +
                    "WHERE DireccionRuta.IdDireccionRuta = @id";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                           
                            return Ok(new string[]
                            {
                                ((int)lector["idDireccionRuta"]).ToString(),
                                (string)lector["NombreDireccionRuta"],
                                (string)lector["NombrePais"], 
                                (string)lector["NombreCiudad"]
                            });

                        }
                        lector.Close();
                    }
                }
                return null; 
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

      

        // PUT api/<RutasController>/5
        [HttpPost]
        public IActionResult InsertarRutas(string nombre, int idDireccionRuta, int idChofer, string placa, int idCliente, string descripcion, string inicio, string final, string estadoEntrega)
        {
            try
            {
                DateOnly fechaInicio = DateOnly.Parse(inicio);
                DateOnly fechaFinal = DateOnly.Parse(final);


                this.conexion.Open();
                string querySQL = "INSERT INTO[dbo].Ruta(Nombre, IdDireccionRuta, IdChofer, NumeroPlaca, IdCliente, Descripcion, FechaInicio, FechaFinal, EstadoEntrega)"+
                                "VALUES(@nombre, @idDireccionRuta, @idChofer, @numeroPlaca, @idCliente, @descripcion, @fechaInicio, @fechaFinal, @estadoEntrega)";
     
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@idDireccionRuta", idDireccionRuta);
                    comando.Parameters.AddWithValue("@idChofer", idChofer);
                    comando.Parameters.AddWithValue("@numeroPlaca", placa);
                    comando.Parameters.AddWithValue("@idCliente", idCliente);
                    comando.Parameters.AddWithValue("@descripcion", descripcion);
                    comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@fechaFinal", fechaFinal);
                    comando.Parameters.AddWithValue("@estadoEntrega", estadoEntrega);
                    comando.ExecuteNonQuery();

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
    }
}
