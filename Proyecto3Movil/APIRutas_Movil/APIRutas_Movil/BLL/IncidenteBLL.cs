using APIRutas_Movil.IBLL;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIRutas_Movil.BLL
{
    public class IncidenteBLL : IIncidenteBLL
    {
        private readonly IIncidenteRepository _incidenteRepository;

        public IncidenteBLL(IIncidenteRepository incidenteRepository)
        {
            _incidenteRepository = incidenteRepository;
        }

        public async Task<ResponseIncidente> SP_CrearIncidencia(Incidente incidente)
        {
            try
            {

                // Validación de campos (por si se ocupa)
                /*  if (string.IsNullOrEmpty(incidente.Descripcion) || incidente.FechaHora == null)
                  {
                      var responseModel = new ResponseModel
                      {
                          ErrorCode = -1,
                          ErrorMsg = "La descripción y la fecha son campos obligatorios."
                      };

                      return new ResponseIncidente
                      {
                          Incidente = null,
                          Errores = responseModel
                      };
                  }*/


                // Llamada al repositorio para crear el incidente
                var resultado = await _incidenteRepository.SP_CrearIncidencia(incidente);



                ResponseIncidente responseIncidente = new ResponseIncidente();
                ResponseModel responseModel = new ResponseModel();
                responseModel.ErrorCode = 0;
                responseModel.ErrorMsg = "Incidente creado con éxito";

                responseIncidente.Incidente = resultado;
                responseIncidente.Errores = responseModel;
                return responseIncidente;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
