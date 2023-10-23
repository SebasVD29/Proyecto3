using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace API_Choferes.Controllers, namespace API_

{
    public class DataBaseController
    {
        

        public string StringConexion()
        {

            string stringConn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            

            return stringConn;
            
        }

      
    }
}
