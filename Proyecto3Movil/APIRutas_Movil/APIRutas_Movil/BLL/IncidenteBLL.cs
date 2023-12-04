using APIRutas_Movil.IBLL;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIRutas_Movil.BLL
{
    public class IncidenteBLL : IincidenteBLL
    {
        private readonly IIncidenteRepository _incidenteRepository;

        public IncidenteBLL(IIncidenteRepository incidenteRepository)
        {
            _incidenteRepository = incidenteRepository;
        }

        public async Task<ResponseIncidente> CrearIncidente(Incidente incidente)
        {
            try
            {
                // Lógica de negocio antes de crear el incidente (si es necesario)
               // if (IncidenteYaExiste(incidente))
               /* {
                    var responseModel = new ResponseModel
                    {
                        ErrorCode = -1,
                        ErrorMsg = "Ya existe un incidente similar en la base de datos."
                    };

                    return new ResponseIncidente
                    {
                        Incidente = null,
                        Errores = responseModel
                    };
                }*/

                // Validación de campos (puedes agregar más validaciones según tus requisitos)
                if (string.IsNullOrEmpty(incidente.Descripcion) || incidente.FechaHora == null)
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
                }

                // Llamada al repositorio para crear el incidente
                var resultado = await _incidenteRepository.SP_CrearIncidencia(incidente);

                // Lógica de negocio después de crear el incidente (si es necesario)
                RealizarAccionesDespuesDeCrearIncidente(resultado);

                // Configurar la respuesta exitosa
                return new ResponseIncidente
                {
                    Incidente = resultado,
                    Errores = null
                };
            }
            catch (Exception ex)
            {
                // Puedes realizar un manejo más específico de la excepción si es necesario
                // Configurar la respuesta de error
                var responseModel = new ResponseModel
                {
                    ErrorCode = -1,
                    ErrorMsg = "Error al procesar la solicitud: " + ex.Message
                };

                return new ResponseIncidente
                {
                    Incidente = null,
                    Errores = responseModel
                };
            }
        }

        /*private bool IncidenteYaExiste(Incidente incidente)
        {
            // Lógica para verificar si un incidente similar ya existe en la base de datos (ejemplo)
            var incidentesSimilares = _incidenteRepository.ObtenerIncidentesSimilares(incidente);
            return incidentesSimilares.Any();
        }*/

        private void RealizarAccionesDespuesDeCrearIncidente(Incidente incidenteCreado)
        {
            // Ejemplo de lógica de negocio después de crear el incidente
            // Puedes realizar acciones adicionales, como enviar notificaciones, actualizar otras entidades, etc.

            // Guardar registro en un log (ejemplo)
            RegistrarLog($"Incidente creado. ID: {incidenteCreado.IdentificadorIncidente}, Descripción: {incidenteCreado.Descripcion}");

            // Enviar notificación (ejemplo)
            EnviarNotificacion(incidenteCreado);

            // Puedes agregar más acciones según tus necesidades específicas
        }

        private void RegistrarLog(string mensaje)
        {
            // Lógica para registrar información de registro (ejemplo)
            Console.WriteLine($"Registro: {mensaje}");
        }

        private void EnviarNotificacion(Incidente incidente)
        {
            // Lógica para enviar notificación (ejemplo)
            Console.WriteLine($"Notificación enviada para el incidente {incidente.IdentificadorIncidente}: Nuevo incidente creado");
        }
    }
}
