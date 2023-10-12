using API_Choferes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Camiones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class camionesControllers : ControllerBase
    {
    


        int count = 0;
        SqlConnection? sqlConnection;

        // GET: api/<camionesControllers>
        [HttpGet]
        public IEnumerable<string> Get()
        {         
            return new string[] { "value1", "value2" };
        }

        // GET api/<camionesControllers>/5
        [HttpGet("{NumeroPlaca}")]
        public string[] Get(int numeroPlaca)
        {
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            try
            {
                string[] returnValues = new string[100];
                //int counter = 0;
                sqlConnection.Open();
                string querySQL = "Select * from dbo.Camiones where numerPlaca = " + numeroPlaca;

                using (SqlCommand comando = new SqlCommand(querySQL, sqlConnection))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            return new string[] { (string)lector["Marca"], (string)lector["Modelo"] };

                        }

                        lector.Close();
                    }
                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return new string[] { "error", "error" };
        }

        // POST api/<camionesControllers>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void Post(int numeroPlaca, string Marca, string Modelo, string AñoFabricacion, string Estado)
        {
            DateTime fecha = DateTime.Now;
           
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            try
            {
                sqlConnection.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "INSERT INTO dbo.Camiones(numeroPlaca, Marca, Modelo, AñoFabricacion, Estado) " +
                    "VALUES (@numeroPlaca, @Marca, @Modelo, @AñoFabricacion, @password, @Estado)";

                using (SqlCommand comando = new SqlCommand(querySQL, sqlConnection))
                {
             
                    comando.Parameters.AddWithValue("numero de placa", numeroPlaca);
                    comando.Parameters.AddWithValue("Marca", Marca);
                    comando.Parameters.AddWithValue("Modelo", Modelo);
                    comando.Parameters.AddWithValue("Año de fabricacion", AñoFabricacion);
                    comando.Parameters.AddWithValue("Estado", Estado);
                    comando.ExecuteNonQuery();


                    /*SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    { }
                    reader.Close();*/
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return;
        }

        // PUT api/<camionesControllers>/5
        [HttpPut("{numeroPlaca}")]
        public void Put(int numeroPlaca, string Marca, string Modelo, string AñoFabricacion,  string Estado)
        {
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            try
            {
               
                sqlConnection.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "UPDATE dbo.Camiones SET  Marca = @Marca, Modelo = @Modelo, Estado = @Estado " +
                    "WHERE  numeroPlaca = " + numeroPlaca;

                using (SqlCommand comando = new SqlCommand(querySQL, sqlConnection))
                {
                    
                    comando.Parameters.AddWithValue("numero de Placa", numeroPlaca);
                    comando.Parameters.AddWithValue("Marca", Marca);
                    comando.Parameters.AddWithValue("Modelo", Modelo);
                    comando.Parameters.AddWithValue("Estado", Estado);
                    comando.ExecuteNonQuery();


                   
                }
                sqlConnection.Close();

                /*SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                { }
                reader.Close();*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return;


        }

        // DELETE api/<camionesControllers>/5
        [HttpDelete("{numeroPlaca}")]
        public void Delete(int numeroPlaca)
        {
        }
    }
}
