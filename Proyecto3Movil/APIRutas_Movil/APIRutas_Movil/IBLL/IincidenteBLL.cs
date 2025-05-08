using APIRutas_Movil.Modelo;
using System.Threading.Tasks;

namespace APIRutas_Movil.IBLL
{
    public interface IIncidenteBLL
    {
        public Task<ResponseIncidente> CrearIncidencia(Incidente nuevoIncidente);
    }
}
