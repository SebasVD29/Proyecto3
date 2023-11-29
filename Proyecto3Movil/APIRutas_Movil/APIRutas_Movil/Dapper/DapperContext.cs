using APIRutas_Movil.IDapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIRutas_Movil.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _iConfiguration;
        private readonly string? _cadenaconexion;
        public DapperContext(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            _cadenaconexion = _iConfiguration.GetConnectionString("API");
        }
        public IDbConnection CrearConexion()
        {
            return new SqlConnection(_cadenaconexion);
        }
    }
}
