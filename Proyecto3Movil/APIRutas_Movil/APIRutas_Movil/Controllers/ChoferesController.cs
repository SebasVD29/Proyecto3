using APIRutas_Movil.IBLL;
using APIRutas_Movil.Modelo;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIRutas_Movil.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoferesController : ControllerBase
    {
        private readonly IChoferBLL _choferBILL;
        public ChoferesController(IChoferBLL choferBLL) 
        {
            _choferBILL = choferBLL;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ResponseChofer>> LoginChofer(Chofer chofer)
        {
            try
            {
                return await _choferBILL.LoginChofer(chofer.Contraseña, chofer.Email);

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
