using APIRutas_Movil.Modelo;
namespace APIRutas_Movil.IRepositorySQL
{
    public interface IIncidenteRepository
    {
        public Task<Incidente> SP_CrearIncidencia(Rutas idRutas, string descripcion, DateOnly fechaHora, string solucion);
    }
}
