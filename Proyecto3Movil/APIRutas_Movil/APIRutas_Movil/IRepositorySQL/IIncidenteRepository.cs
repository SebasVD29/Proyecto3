using APIRutas_Movil.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIRutas_Movil.IRepositorySQL
{
    public interface IIncidenteRepository
    {
        Task<Incidente> SP_CrearIncidencia(Incidente incidente);
       // Task<IEnumerable<Incidente>> ObtenerIncidentesSimilares(Incidente incidente);
    }
}
