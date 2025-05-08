using APIRutas_Movil.IBLL;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using System.Diagnostics;

namespace APIRutas_Movil.BLL
{
    public class RutasBLL : IRutasBLL

    {
        private readonly IRutasRepository _rutasRepository;

        public RutasBLL(IRutasRepository rutasRepository)
        {
            _rutasRepository = rutasRepository;
        }

        public async Task<ResponseListaRutas> ListarRutasPorChofer(int idChofer)
        {
            try
            {

                 var rutas = await _rutasRepository.ListarRutasPorChofer(idChofer);
            

                ResponseListaRutas responseListaRutas = new ResponseListaRutas();
                ResponseModel responseModel = new ResponseModel();

                responseListaRutas.ruta = rutas.ToList();
                responseListaRutas.errores.errorcode = 0;
                responseListaRutas.errores.errormsg = "Lista de Rutas encontrada";
                return responseListaRutas;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<ResponseRutas> CambioEstado(Rutas rutas)
        {
            try
            {
                var rutaActualizado = await _rutasRepository.CambioEstado(rutas);
                ResponseRutas responseRuta = new ResponseRutas();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = 0;
                responseModel.errormsg = "Estado de la Ruta Actualizado con éxito";

                responseRuta.ruta = rutaActualizado;
                responseRuta.errores = responseModel;
                return responseRuta;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
