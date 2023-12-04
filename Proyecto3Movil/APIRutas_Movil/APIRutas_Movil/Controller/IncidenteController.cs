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
                if (nuevoIncidente == null)
                {
                    // Manejar el caso donde nuevoIncidente es nulo
                    return BadRequest("La solicitud es inválida. El objeto nuevoIncidente no puede ser nulo.");
                }

                // Aquí puedes llamar a tu lógica de negocio para crear el incidente
                var resultado = await _incidenteBLL.CrearIncidente(nuevoIncidente);

                if (resultado == null || resultado.Incidente == null)
                {
                    // Manejar el caso donde el resultado o el incidente en el resultado son nulos
                    return BadRequest("Error al crear el incidente. No se recibió una respuesta válida.");
                }

                // Puedes devolver un código 201 Created si la creación fue exitosa
                return CreatedAtAction(nameof(CrearIncidente), new { id = resultado.Incidente.IdentificadorIncidente }, resultado);
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes personalizar según tus necesidades
                ResponseIncidente response = new ResponseIncidente();
                // Configura la respuesta de error según tus necesidades
                return BadRequest($"Error al procesar la solicitud: {ex.Message}");
            }
        }
    }
}