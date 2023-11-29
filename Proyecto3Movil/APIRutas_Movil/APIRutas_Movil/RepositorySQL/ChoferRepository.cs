using APIRutas_Movil.IDapper;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using Dapper;
using System.Data;

namespace APIRutas_Movil.RepositorySQL
{
    public class ChoferRepository : IChoferRepository
    {
        private readonly IDapperContext _context;

        // inyectamos la interfaz de IDapperContext para poder obtener cadena de conexion en cada método
        public ChoferRepository(IDapperContext context)
        {
            _context = context;
        }


        public async Task<Chofer> SP_LoginChofer(string password, string email)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@Password", password, DbType.String, ParameterDirection.Input);
                param.Add("@Email", email, DbType.String, ParameterDirection.Input);
                using (var conn = _context.CrearConexion())
                {
                    return await conn.QuerySingleOrDefaultAsync<Chofer>("SP_LoginChofer", param, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
