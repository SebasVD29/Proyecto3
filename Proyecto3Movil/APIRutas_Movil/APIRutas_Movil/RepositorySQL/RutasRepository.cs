using APIRutas_Movil.IDapper;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using Dapper;
using System.Data;

namespace APIRutas_Movil.RepositorySQL
{
    public class RutasRepository : IRutasRepository
    {

        private readonly IDapperContext _context;

        // inyectamos la interfaz de IDapperContext para poder obtener cadena de conexion en cada método
        public RutasRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rutas>> SP_ListarRutasPorChofer(Chofer idChofer)
        {

        }

        public async Task<Rutas> SP_ActualizarEstadoRuta(Rutas idRuta, string estadoEntrega)
        {

        }
    }
}
