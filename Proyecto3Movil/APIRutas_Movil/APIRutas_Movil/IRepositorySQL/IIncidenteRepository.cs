using APIRutas_Movil.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIRutas_Movil.IRepositorySQL
{
    public interface IIncidenteRepository
    {
        public Task<Incidente> CrearIncidencia(Incidente incidente);
     
    }
}
