using API_Choferes.Models;
using API_Choferes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web.Http.Cors;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;

namespace API_Choferes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class CamionesController : ControllerBase
    {
        private readonly CamionesService _camionesService;
        private securityController securityController;
        private DataBaseController dataBase;
        private SqlConnection conexion;

        public CamionesController(CamionesService camionesService)
        {
            _camionesService = camionesService;
            this.dataBase = new DataBaseController();
            this.securityController = new securityController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }

        [HttpGet]
        public string[][] Get()
        {
            try
            {
                this.conexion.Open();

                // Cantidad de rutas 
                string querySQL = "Select * from dbo.Camiones";
                int cantidadChoferes = 0;
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            cantidadChoferes++;
                        }
                        lector.Close();
                    }

                }
                string[][] returnValues = new string[cantidadChoferes][];
                int contador = 0;
                querySQL = "Select * from dbo.Camiones";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            // Devolver array en lugar de primer elemento 
                            returnValues[contador] = new string[] { ((String)lector["numeroPlaca"])};
                            contador++;
                        }
                        return returnValues;

                        lector.Close();
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

        [HttpGet("{numeroPlaca}")]
        public IActionResult Get(string numeroPlaca)
        {
            try
            {
                var camiones = _camionesService.GetByNumeroPlaca(numeroPlaca);
                return Ok(camiones);
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

        [HttpPost]
        public IActionResult Post([FromBody] CamionModel camion)
        {
            try
            {
                _camionesService.InsertCamion(camion);
                return Ok(new { Message = "Operación exitosa al Insertar Camiones" });

                /*return Ok(new { Message = $"Camión agregado correctamente: " +
                    $"{camion.Marca} " +
                    $"{camion.Modelo} " +
                    $"(Placa: {camion.numeroPlaca})",
                    Camion = camion });
                }*/
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

        [HttpPut("{numeroPlaca}")]
        public IActionResult Put(string numeroPlaca, [FromBody] CamionModel camion)
        {
            try
            {

               _camionesService.UpdateCamion(numeroPlaca,
                                             camion.Marca,
                                             camion.Modelo,
                                             camion.Fabricacion,
                                             camion.Estado);

                return Ok(new { Message = "Operación exitosa al Actualizas Camiones" });
                /* return Ok(new { Message = $"Camión actualizado correctamente:" +
                     $" {camion.Marca} {camion.Modelo} (Placa: " +
                     $"{camion.numeroPlaca})", 
                     Camion = camion });*/


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
