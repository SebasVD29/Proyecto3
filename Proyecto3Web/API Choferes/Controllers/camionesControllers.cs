using API_Choferes.Models;
using API_Choferes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web.Http.Cors;

namespace API_Choferes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class CamionesController : ControllerBase
    {
        private readonly CamionesService _camionesService;

        public CamionesController(CamionesService camionesService)
        {
            _camionesService = camionesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var camiones = _camionesService.Get();
            return Ok(camiones);
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
                return Ok($"Camión agregado correctamente: {camion.Marca} {camion.Modelo} (Placa: {camion.numeroPlaca})");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar el camión: {ex.Message}");
            }
        }

        [HttpPut("{numeroPlaca}")]
        public IActionResult Put(string numeroPlaca, [FromBody] CamionModel camion)
        {
            _camionesService.UpdateCamion(numeroPlaca, camion.Marca, camion.Modelo, camion.Fabricacion, camion.Estado);
            return Ok($"Camión actualizado correctamente: {camion.Marca} {camion.Modelo} (Placa: {camion.numeroPlaca})");
        }
    }
}
