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

        public async Task<ResponseRutas> ListarRutasPorChofer(int idChofer)
        {
            try
            {

                 var rutas = await _rutasRepository.ListarRutasPorChofer(idChofer);
            

                ResponseRutas responseRutas = new ResponseRutas();
                ResponseModel responseModel = new ResponseModel();

                responseRutas.ruta = rutas.ToList();
                responseRutas.errores.errorcode = 0;
                responseRutas.errores.errormsg = "Lista de Rutas encontrada";
                return responseRutas;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<Boolean> CambioEstado(Rutas rutas)
        {
            try
            {
                var rutaActualizado = await _rutasRepository.CambioEstado(rutas);
                ResponseRutas responseRuta = new ResponseRutas();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = 0;
                responseModel.errormsg = "Estado de la Ruta Actualizado con éxito";

               
                //responseRuta.ruta = rutaActualizado;
                responseRuta.errores = responseModel;
                return rutaActualizado;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
