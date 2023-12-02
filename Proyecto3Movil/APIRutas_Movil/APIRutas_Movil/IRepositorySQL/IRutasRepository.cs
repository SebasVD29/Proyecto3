using APIRutas_Movil.Modelo;

namespace APIRutas_Movil.IRepositorySQL
{
    public interface IRutasRepository
    {
        public Task<IEnumerable<Rutas>> SP_ListarRutasPorChofer(Chofer idChofer);

        public Task<Rutas> SP_ActualizarEstadoRuta(int idRuta, string estadoEntrega);
    }
}
