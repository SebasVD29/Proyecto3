using APIRutas_Movil.IBLL;
using APIRutas_Movil.Modelo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APIRutas_Movil.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenteController : ControllerBase
    {
        private readonly IincidenteBLL _incidenteBLL;

        public IncidenteController(IincidenteBLL incidenteBLL)
        {
            _incidenteBLL = incidenteBLL;
        }

        [HttpPost]
        [Route("CrearIncidente")]
        public async Task<ActionResult<ResponseIncidente>> CrearIncidente([FromBody] Incidente nuevoIncidente)
        {
            try
            {
                // Aquí puedes llamar a tu lógica de negocio para crear el incidente
                var resultado = await _incidenteBLL.CrearIncidente(nuevoIncidente);

                // Puedes devolver un código 201 Created si la creación fue exitosa
                return CreatedAtAction(nameof(CrearIncidente), new { id = resultado.Incidente.IdentificadorIncidente }, resultado);
            }
            catch (Exception)
            {
                // Manejo de errores, puedes personalizar según tus necesidades
                ResponseIncidente response = new ResponseIncidente();
                // Configura la respuesta de error según tus necesidades
                return new JsonResult(response);
            }
        }
    }
}
