﻿using APIRutas_Movil.IBLL;
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

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult<ResponseChofer>> LoginChofer(string password, string email)
        {
            try
            {
                return await _choferBILL.LoginChofer(password, email);

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
