using APIRutas_Movil.IBLL;
using APIRutas_Movil.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIRutas_Movil.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenteController : ControllerBase
    {
        private readonly IIncidenteBLL _incidenteBLL;

        public IncidenteController(IIncidenteBLL incidenteBLL)
        {
            _incidenteBLL = incidenteBLL;
        }

        [HttpPost]
        [Route("CrearIncidencia")]
        public async Task<ActionResult<ResponseIncidente>> CrearIncidencia(Incidente incidente)
        {
            try
            {
                var response = await _incidenteBLL.CrearIncidencia(incidente);
                return new JsonResult(response);
            }
            catch (Exception)
            {
                ResponseIncidente responseIncidente = new ResponseIncidente();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = -1;
                responseModel.errormsg = "Error al insertar una Incidencia";
                responseIncidente.errores = responseModel;
                return new JsonResult(responseIncidente);
            }
        }

    }
}
