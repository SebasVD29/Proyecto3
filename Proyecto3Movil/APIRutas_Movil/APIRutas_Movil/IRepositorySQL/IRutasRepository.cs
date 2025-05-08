using APIRutas_Movil.Modelo;

namespace APIRutas_Movil.IRepositorySQL
{
    public interface IRutasRepository
    {
        public Task<IEnumerable<Rutas>> ListarRutasPorChofer(int idChofer);

        public Task<Rutas> CambioEstado( Rutas ruta);
    }
}
