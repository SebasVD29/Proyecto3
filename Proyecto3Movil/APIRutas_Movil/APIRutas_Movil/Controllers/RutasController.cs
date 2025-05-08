
using APIRutas_Movil.BLL;
using APIRutas_Movil.IBLL;
using APIRutas_Movil.Modelo;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;


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

        [HttpGet("ListaRutasPorChofer/{IdChofer}")]
        //[Route("ListaRutasPorChofer")]
        public async Task<ResponseListaRutas> ListarRutasPorChofer(int IdChofer)
        {
            try
            {
                var test =  await _rutasBILL.ListarRutasPorChofer(IdChofer);
                return test;
            }
            catch (Exception)
            {

                ResponseListaRutas responseListaRutas = new ResponseListaRutas();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = -1;
                responseModel.errormsg = "Error al listar las rutas por chofer";
                responseListaRutas.errores = responseModel;
                return responseListaRutas;
            }
        }

        [AcceptVerbs("POST", "PUT")]
        [Route("ActualizarEstado")]
        public async Task<ActionResult<ResponseRutas>> CambioEstado(Rutas rutas)
        {
            try
            {

                var response = await _rutasBILL.CambioEstado(rutas);
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
