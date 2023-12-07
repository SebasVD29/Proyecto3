using APIRutas_Movil.IDapper;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;

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
            try
            {
                var param = new DynamicParameters();

                param.Add("@IdRuta", incidente.IdRuta, DbType.String, ParameterDirection.Input);
                param.Add("@descripcion", incidente.Descripcion, DbType.String, ParameterDirection.Input);
                param.Add("@fecha", incidente.FechaHora, DbType.Date, ParameterDirection.Input);
                param.Add("@solucion", incidente.Solucion, DbType.String, ParameterDirection.Input);

                using (var conn = _context.CrearConexion())
                {
                    Incidente _incidente = await conn.QuerySingleAsync<Incidente>("SP_CrearIncidencia", param, commandType: CommandType.StoredProcedure);
                    return _incidente;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
