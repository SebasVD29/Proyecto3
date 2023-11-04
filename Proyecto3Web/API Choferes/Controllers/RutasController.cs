using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Choferes.Controllers
{
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    [ApiController]
    public class RutasController : ControllerBase
    {
        private securityController securityController;
        private DataBaseController dataBase;
        private SqlConnection conexion;

        public RutasController()
        {
            this.dataBase = new DataBaseController();
            this.securityController = new securityController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }

        // GET: api/<RutasController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Get rutas por cliente
        // GET api/<RutasController>/5
        [HttpGet("{id}")]
        public string[] Get(int id)
        {
            try
            {
                string[] returnValues = new string[100];
                //int counter = 0;

                this.conexion.Open();

                string querySQL = "Select * from dbo.DireccionRuta where Cliente = @id";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {                          
                            return new string[] { ((int)lector["idDireccionRuta"]).ToString(), (string)lector["NombreDireccionRuta"] };
                        }

                        lector.Close();
                    }

                }

                this.conexion.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return new string[] { "error", "error" };
        }

        // POST api/<RutasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RutasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RutasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
