
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
        [Route("RutasPorChofer")]
        public async Task<List<ResponseRutas>> ListarRutasPorChofer(int IdChofer)
        {
            try
            {
                List<ResponseRutas> test = await _rutasBILL.ListarRutasPorChofer(IdChofer);
                return test;
            }
            catch (Exception)
            {

                ResponseChofer responseChofer = new ResponseChofer();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = -1;
                responseModel.errormsg = "Error al buscar las rutas";
                responseChofer.errores = responseModel;
                return null;
            }
        }

        [HttpPut]
        [Route("CambioEstado")]
        public async Task<ActionResult<Boolean>> CambioEstado(string EstadoEntrega, int IdentificadorRuta)
        {
            try
            {
                return await _rutasBILL.CambioEstado(EstadoEntrega, IdentificadorRuta);

            }
            catch (Exception)
            {

                ResponseChofer responseChofer = new ResponseChofer();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = -1;
                responseModel.errormsg = "Error al buscar el chofer";
                responseChofer.errores = responseModel;
                return new JsonResult(responseChofer);
            }
        }
    }
}
