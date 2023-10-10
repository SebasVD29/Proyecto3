using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        int count = 0;
        SqlConnection sqlConnection;

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
                int counter = 0;
                sqlConnection.Open();
                string queryString = "Select * from dbo.Chofer where IdentificadorChofer =" + id;
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return new string[] { (string)reader["Nombre"], (string)reader["Email"] };

                }
                reader.Close();
                sqlConnection.Close();
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
        public void Post(int identificacion, string nombre, string apellidos, string email, string contrasena, string fecha, string estado)
        {
            string sqlconn = $"Server=tcp:proyecto3ulatina.database.windows.net,1433;Initial Catalog=plogisticsdatabase;Persist Security Info=False;User ID=julihr;Password=Belfast0101.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            sqlConnection = new SqlConnection(sqlconn);

            try
            {
                string[] returnValues = new string[100];
                sqlConnection.Open();
                string queryString = 
                    "INSERT INTO dbo.Chofer(IdentificadorChofer, Nombre, Apellido, Email, Contraseña, FechaRegistro, Estado) " +
                    "VALUES (" + identificacion + ",'" + nombre + "','" + apellidos + "','" + email + "','" +contrasena + "','" + fecha + "','" + estado + "');";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                { }
                reader.Close();
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<choferesControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
