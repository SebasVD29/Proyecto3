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
        [Route("SP_CrearIncidencia")]
        public async Task<ActionResult<ResponseIncidente>> SP_CrearIncidencia( Incidente nuevoIncidente)
        {
            try
            {
                var response = await _incidenteBLL.SP_CrearIncidencia(nuevoIncidente);
                return new JsonResult(response);
            }
            catch (Exception)
            {
                ResponseIncidente responseIncidente = new ResponseIncidente();

                ResponseModel responseModel = new ResponseModel();
                responseModel.ErrorCode = -1;
                responseModel.ErrorMsg = "Error al insertar el incidente";
                responseIncidente.Errores = responseModel;
                return new JsonResult(responseIncidente);
            }
        }
    }
}