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

        public async Task<ResponseIncidente>SP_CrearIncidencia(Incidente incidente)
        {
            try
            {

                // Llamada al repositorio para crear el incidente
                var resultado = await _incidenteRepository.SP_CrearIncidencia(incidente);



                ResponseIncidente responseIncidente = new ResponseIncidente();
                ResponseModel responseModel = new ResponseModel();
                responseModel.errorcode = 0;
                responseModel.errormsg = "Incidente creado con éxito";

                responseIncidente.incidente = resultado;
                responseIncidente.errores = responseModel;
                return responseIncidente;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
