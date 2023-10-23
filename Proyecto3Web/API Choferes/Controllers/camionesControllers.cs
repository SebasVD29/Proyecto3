using API_Choferes.Models;
using API_Choferes.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace API_Choferes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class camionesControllers : ControllerBase
    {
        private readonly CamionesService _camionesService;

        public camionesControllers(CamionesService camionesService)
        {
            _camionesService = camionesService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _camionesService.Get();
        }

        [HttpGet("{numeroPlaca}")]
        public IEnumerable<string> Get(string numeroPlaca)
        {
            return _camionesService.GetByNumeroPlaca(numeroPlaca);
        }

        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public IActionResult Post([FromBody] CamionModel camion)
        {
            try
            {
                _camionesService.InsertCamion(camion);
                return Ok();  // Indica que la operación fue exitosa
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }

        [HttpPut("{numeroPlaca}")]
        public void Put(string numeroPlaca, string Marca, string Modelo, DateTime Fabricacion, string Estado)
        {
            _camionesService.UpdateCamion(numeroPlaca, Marca, Modelo, Fabricacion, Estado);
        }

        [HttpDelete("{numeroPlaca}")]
        public void Delete(string numeroPlaca)
        {
            _camionesService.InactivarCamion(numeroPlaca);
        }
    }
}
