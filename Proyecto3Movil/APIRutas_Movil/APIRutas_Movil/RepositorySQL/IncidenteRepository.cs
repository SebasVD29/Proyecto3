using APIRutas_Movil.IDapper;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using Dapper;
using System.Data;
namespace APIRutas_Movil.RepositorySQL
{
    public class IncidenteRepository : IIncidenteRepository
    {

        private readonly IDapperContext _context;

        // inyectamos la interfaz de IDapperContext para poder obtener cadena de conexion en cada método
        public IncidenteRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<Incidente> SP_CrearIncidencia(Incidente incidente)
        {

        }
    }
}
