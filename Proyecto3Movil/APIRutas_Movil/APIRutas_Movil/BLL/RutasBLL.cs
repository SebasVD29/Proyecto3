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

        public async Task<List<ResponseRutas>> ListarRutasPorChofer(int idChofer)
        {
            try
            {

                IEnumerable<Rutas> rutas = await _rutasRepository.ListarRutasPorChofer(idChofer);
                List<ResponseRutas> listaRutas = new List<ResponseRutas>(); ;

                ResponseRutas ResponseRutas = new ResponseRutas();
                ResponseModel responseModel = new ResponseModel();

                Dictionary<string, ResponseRutas> d = new Dictionary<string, ResponseRutas>();
                if (rutas != null) { 
                       for (int i = 0; i < rutas.Count(); i++) {
                       d.Add("Variable"+i, new ResponseRutas());
                    }
                }

                if (rutas != null)
                {
                    for (int i = 0; i < rutas.Count(); i++)
                        {
                        d.ElementAt(i).Value.ruta.IdentificadorRuta = rutas.ElementAt(i).IdentificadorRuta;
                        d.ElementAt(i).Value.ruta.NombreRuta = rutas.ElementAt(i).NombreRuta;

                        d.ElementAt(i).Value.ruta.NombreDireccionRuta = rutas.ElementAt(i).NombreDireccionRuta;
                        d.ElementAt(i).Value.ruta.PaisFinal = rutas.ElementAt(i).PaisFinal;
                        d.ElementAt(i).Value.ruta.CiudadFinal = rutas.ElementAt(i).CiudadFinal;

                        d.ElementAt(i).Value.ruta.IdChofer = rutas.ElementAt(i).IdChofer;
                        d.ElementAt(i).Value.ruta.NumeroPlaca = rutas.ElementAt(i).NumeroPlaca;
                        d.ElementAt(i).Value.ruta.IdCliente = rutas.ElementAt(i).IdCliente;

                        d.ElementAt(i).Value.ruta.NombreCliente = rutas.ElementAt(i).NombreCliente;
                        d.ElementAt(i).Value.ruta.TelefonoCliente = rutas.ElementAt(i).TelefonoCliente;

                        d.ElementAt(i).Value.ruta.Descripcion = rutas.ElementAt(i).Descripcion;
                        d.ElementAt(i).Value.ruta.FechaInicio = rutas.ElementAt(i).FechaInicio;
                        d.ElementAt(i).Value.ruta.FechaFinal = rutas.ElementAt(i).FechaFinal;
                        d.ElementAt(i).Value.ruta.EstadoEntrega = rutas.ElementAt(i).EstadoEntrega;
                        listaRutas.Add(d.ElementAt(i).Value);
                    }                                    
                }
                else
                {
                    responseModel.errorcode = 1;
                    responseModel.errormsg = "No se ha podido encontrar las rutas";
                }

                ResponseRutas.errores = responseModel;
                return listaRutas;
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
