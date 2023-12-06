
using APIRutas_Movil.BLL;
using APIRutas_Movil.IBLL;
using APIRutas_Movil.Modelo;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace APIRutas_Movil.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutasController : ControllerBase
    {
        private readonly IRutasBLL _rutasBILL;
        public RutasController(IRutasBLL rutasBLL)
        {
            _rutasBILL = rutasBLL;
        }

        [HttpGet]
        [Route("ListaRutasPorChofer")]
        public async Task<List<ResponseRutas>> ListarRutasPorChofer(int IdChofer)
        {
            try
            {
                List<ResponseRutas> test = await _rutasBILL.ListarRutasPorChofer(IdChofer);
                return test;
            }
            catch (Exception)
            {

                ResponseRutas responseRutas = new ResponseRutas();
                //ResponseListaRutas responseRutas = new ResponseListaRutas();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = -1;
                responseModel.errormsg = "Error al listar las rutas por chofer";
                responseRutas.errores = responseModel;
                return null;
            }
        }

        [AcceptVerbs("POST", "PUT")]
        [Route("ActualizarEstado")]
        public async Task<ActionResult<Boolean>> CambioEstado(string estadoEntrega, int identificadorRuta)
        {
            try
            {

                var response = await _rutasBILL.CambioEstado(estadoEntrega, identificadorRuta);
                return new JsonResult(response);
            }
            catch (Exception)
            {

                ResponseRutas responseRutas = new ResponseRutas();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = -1;
                responseModel.errormsg = "Error al actulizar el estado de entrega de la Ruta";
                responseRutas.errores = responseModel;
                return new JsonResult(responseRutas);
            }
        }
    }
}
