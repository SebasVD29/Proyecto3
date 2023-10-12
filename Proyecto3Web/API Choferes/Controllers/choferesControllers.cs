using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Choferes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class choferesControllers : ControllerBase
    {
        string stringEncriptada = "";
        string stringDesencriptada = "";
        private securityController securityController = new securityController();


        int count = 0;
        SqlConnection? sqlConnection;

        // GET: api/<choferesControllers>
        [HttpGet]
        public IEnumerable<string> Get()
        {         
            return new string[] { "value1", "value2" };
        }

        // GET api/<choferesControllers>/5
        [HttpGet("{id}")]
        public string[] Get(int id)
        {
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            try
            {
                string[] returnValues = new string[100];
                //int counter = 0;
                sqlConnection.Open();
                string querySQL = "Select * from dbo.Chofer where IdentificadorChofer = " + id;

                using (SqlCommand comando = new SqlCommand(querySQL, sqlConnection))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            return new string[] { (string)lector["Nombre"], (string)lector["Email"] };

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

        // POST api/<choferesControllers>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void Post(int identificacion, string nombre, string apellidos, string email, string contrasena, string estado)
        {
            DateTime fecha = DateTime.Now;
            stringEncriptada = securityController.EncriptarBase64(contrasena);
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            try
            {
                sqlConnection.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "INSERT INTO dbo.Chofer(IdentificadorChofer, Nombre, Apellido, Email, Contraseña, FechaRegistro, Estado) " +
                    "VALUES (@idetificador, @nombre, @apellidos, @email, @password, @fecha, @estado)";

                using (SqlCommand comando = new SqlCommand(querySQL, sqlConnection))
                {
                    comando.Parameters.AddWithValue("identificador", identificacion);
                    comando.Parameters.AddWithValue("nombre", nombre);
                    comando.Parameters.AddWithValue("apellidos", apellidos);
                    comando.Parameters.AddWithValue("email", email);
                    comando.Parameters.AddWithValue("password", stringEncriptada);
                    comando.Parameters.AddWithValue("fecha", fecha);
                    comando.Parameters.AddWithValue("estado", estado);
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

        // PUT api/<choferesControllers>/5
        [HttpPut("{id}")]
        public void Put(int identificacion, string nombre, string apellidos, string email, string contrasena, string estado)
        {
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            try
            {
                stringEncriptada = securityController.EncriptarBase64(contrasena);
                sqlConnection.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "UPDATE dbo.Chofer SET  Nombre = @nombre, Apellido = @apellidos, Email = @email, Contraseña = @password, Estado = @estado " +
                    "WHERE  IdentificadorChofer = " + identificacion;

                using (SqlCommand comando = new SqlCommand(querySQL, sqlConnection))
                {
                    
                    comando.Parameters.AddWithValue("nombre", nombre);
                    comando.Parameters.AddWithValue("apellidos", apellidos);
                    comando.Parameters.AddWithValue("email", email);
                    comando.Parameters.AddWithValue("password", stringEncriptada);
                    comando.Parameters.AddWithValue("estado", estado);
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

        // DELETE api/<choferesControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
