using APIRutas_Movil.IDapper;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using Dapper;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Rutas>> ListarRutasPorChofer(int idChofer)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@idChofer", idChofer, DbType.Int64, ParameterDirection.Input);
                using (var conn = _context.CrearConexion())
                {
                    return await conn.QueryAsync<Rutas>("SP_ListarRutasPorChofer", param, commandType: CommandType.StoredProcedure);
                    
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Boolean> CambioEstado(string estadoEntrega, int identificadorRuta)
        {
            try
            {

                param.Add("@EstadoEntrega", estadoEntrega, DbType.String, ParameterDirection.Input);
                param.Add("@IdRuta", identificadorRuta, DbType.Int64, ParameterDirection.Input);
                using (var conn = _context.CrearConexion())
                {
                    await conn.QueryAsync("SP_ActualizarEstadoRuta", param, commandType: CommandType.StoredProcedure);
                    return true;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

       
    }
}
