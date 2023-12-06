using APIRutas_Movil.Modelo;
namespace APIRutas_Movil.IRepositorySQL
{
    public interface IIncidenteRepository
    {
        public Task<Incidente> CrearIncidencia(Incidente incidente);
       // Task<IEnumerable<Incidente>> ObtenerIncidentesSimilares(Incidente incidente);
    }
}
