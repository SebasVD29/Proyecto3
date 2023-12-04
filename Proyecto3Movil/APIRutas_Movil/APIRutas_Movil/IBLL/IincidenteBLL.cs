using APIRutas_Movil.Modelo;
using System.Threading.Tasks;

namespace APIRutas_Movil.IBLL
{
    public interface IincidenteBLL
    {
        //Task<ResponseIncidente> CrearIncidente(int IdentificadorIncidente, int IdRuta, string Descripcion, string FechaHora, string Solucion);
        Task<ResponseIncidente> CrearIncidente(Incidente nuevoIncidente);
    }
}
