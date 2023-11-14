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
            return new string[] { };
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
                string querySQL = "SELECT  *\r\nFROM [dbo].[Ruta]\r\nINNER JOIN Clientes\r\nON Ruta.IdCliente = Clientes.IdentificadorCliente\r\nWHERE Ruta.IdCliente = @id AND Clientes.Estado = 1";
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
                querySQL = "SELECT  *\r\nFROM [dbo].[Ruta]\r\n" +
                    "INNER JOIN Clientes\r\nON Ruta.IdCliente = Clientes.IdentificadorCliente\r\n" +
                    "WHERE Ruta.IdCliente = @id AND Clientes.Estado = 1";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            // Devolver array en lugar de primer elemento 
                            returnValues[contador] = new string[] { 
                                ((int)lector["idDireccionRuta"]).ToString(), 
                                (string)lector["Nombre"] };
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
