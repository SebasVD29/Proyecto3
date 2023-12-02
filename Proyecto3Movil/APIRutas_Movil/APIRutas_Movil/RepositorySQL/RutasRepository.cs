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
            try
            {
                using (var conn = _context.CrearConexion())
                {
                    return await conn.QueryAsync<Rutas>("lista_clientes");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Rutas> SP_ActualizarEstadoRuta(int idRuta, string estadoEntrega)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@id", idRuta, DbType.String, ParameterDirection.Input);
                param.Add("@nombre", estadoEntrega, DbType.String, ParameterDirection.Input);
      

                using (var conn = _context.CrearConexion())
                {
                    Rutas rutas;
                    await conn.ExecuteAsync("actualizar_cliente", param, commandType: CommandType.StoredProcedure);
                    return rutas;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
