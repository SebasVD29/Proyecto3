using APIRutas_Movil.Modelo;
using System.Threading.Tasks;

namespace APIRutas_Movil.IBLL
{
    public interface IincidenteBLL
    {
        Task<ResponseIncidente> SP_CrearIncidencia(Incidente nuevoIncidente);
    }
}
