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
    public class RutasPorCliente : ControllerBase
    {
        private securityController securityController;
        private DataBaseController dataBase;
        private SqlConnection conexion;
        public RutasPorCliente()
        {
            this.dataBase = new DataBaseController();
            this.securityController = new securityController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }


        // GET: api/<RutasPorCliente>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RutasPorCliente>/5
        // Get rutas por cliente
        // GET api/<RutasController>/5
        [HttpGet("{id}")]
        public string[][] Get(int id)
        {
            try
            {
                this.conexion.Open();

                // Cantidad de rutas 
                string querySQL = "Select * from dbo.DireccionRuta where Cliente = @id";
                int cantidadRutas = 0;
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            cantidadRutas++;
                        }
                        lector.Close();
                    }

                }
                string[][] returnValues = new string[cantidadRutas][];
                int contador = 0;
                querySQL = "Select * from dbo.DireccionRuta where Cliente = @id";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            // Devolver array en lugar de primer elemento 
                            returnValues[contador] = new string[] { ((int)lector["idDireccionRuta"]).ToString(), (string)lector["NombreDireccionRuta"] };
                            contador++;
                        }
                        return returnValues;
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
        }

        // POST api/<RutasPorCliente>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RutasPorCliente>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RutasPorCliente>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
