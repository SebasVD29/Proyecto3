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
            var camiones = _camionesService.GetByNumeroPlaca(numeroPlaca);
            return Ok(camiones);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CamionModel camion)
        {
            try
            {
                _camionesService.InsertCamion(camion);
                return Ok(new { Message = $"Camión agregado correctamente: {camion.Marca} {camion.Modelo} (Placa: {camion.numeroPlaca})", Camion = camion });
                }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar el camión: {ex.Message}");
            }
        }

        [HttpPut("{numeroPlaca}")]
        public IActionResult Put(string numeroPlaca, [FromBody] CamionModel camion)
        {
           _camionesService.UpdateCamion(numeroPlaca,
                                         camion.Marca,
                                         camion.Modelo,
                                         camion.Fabricacion,
                                         camion.Estado);
           return Ok(new { Message = $"Camión actualizado correctamente: {camion.Marca} {camion.Modelo} (Placa: {camion.numeroPlaca})", Camion = camion });

        }
    }
}
