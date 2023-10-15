using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace API_Choferes.Controllers
{
    public class DataBaseController
    {
        

        public SqlConnection StringConexion()
        {
            
            
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conexion = new SqlConnection(sqlconn);


         
            return conexion;
            
        }
    }
}
