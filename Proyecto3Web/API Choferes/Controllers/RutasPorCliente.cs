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
      
        private DataBaseController dataBase;
        private SqlConnection conexion;
        public RutasPorCliente()
        {
            this.dataBase = new DataBaseController();
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
                /*
                 

                SELECT  *\r\nFROM [dbo].[Ruta]" +
                    "\r\nINNER JOIN Clientes\r\nON " +
                    "Ruta.IdentificadorCliente = Clientes.IdentificadorCliente" +
                    "\r\nWHERE Ruta.IdentificadorCliente = @id AND Clientes.Estado = 1";
                 */

                // Cantidad de rutas 
                string querySQL = "SELECT DireccionRuta.NombreDireccionRuta FROM [dbo].Clientes, [dbo].DireccionRuta " +
                    "INNER JOIN [dbo].ClientePais ON " +
                    "DireccionRuta.IdClientePais = ClientePais.IdClientePais " +
                    "WHERE ClientePais.IdCliente = @id " +
                    "AND Clientes.IdentificadorCliente = ClientePais.IdCliente " +
                    "AND Clientes.Estado = 1";
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
                /*
                 SELECT  *\r\nFROM [dbo].[Ruta]" +
                    "\r\nINNER JOIN Clientes\r\nON " +
                    "Ruta.IdentificadorCliente = Clientes.IdentificadorCliente" +
                    "\r\nWHERE Ruta.IdentificadorCliente = @id AND Clientes.Estado = 1"
              
                 SELECT DireccionRuta.NombreDireccionRuta, Pais.Nombre, Ciudad.Nombre  
                FROM dbo.Clientes,dbo.Pais, dbo.Ciudad, [dbo].DireccionRuta 
                INNER JOIN dbo.ClientePais ON 
                DireccionRuta.IdClientePais = ClientePais.IdClientePais 
                INNER JOIN dbo.PaisCiudad ON 
                DireccionRuta.IdPaisCiudad = PaisCiudad.IdPaisCiudad
                WHERE ClientePais.IdCliente = @id 
                AND Clientes.IdentificadorCliente = ClientePais.IdCliente
                AND dbo.PaisCiudad.IdPais = dbo.Pais.IdentificadorPais
                AND dbo.PaisCiudad.IdCiudad = dbo.Ciudad.IdentificadorCiudad
                AND Clientes.Estado = 1;

                ---------------------------------------------------------------------

                

                 */

                querySQL = "SELECT DireccionRuta.idDireccionRuta, DireccionRuta.NombreDireccionRuta, " +
                    "Pais.Nombre as NombrePais, Ciudad.Nombre as NombreCiudad " +
                    "FROM  [dbo].DireccionRuta " +
                    "INNER JOIN [dbo].ClientePais ON " +
                    "DireccionRuta.IdClientePais = ClientePais.IdClientePais " +
                    "INNER JOIN [dbo].PaisCiudad ON " +
                    "DireccionRuta.IdPaisCiudad = PaisCiudad.IdPaisCiudad " +
                    "INNER JOIN [dbo].Pais ON " +
                    "Pais.IdentificadorPais = PaisCiudad.IdPais " +
                    "INNER JOIN [dbo].Ciudad ON " +
                    "Ciudad.IdentificadorCiudad = PaisCiudad.IdCiudad " +
                    "INNER JOIN [dbo].Clientes ON " +
                    "Clientes.IdentificadorCliente = ClientePais.IdCliente " +
                    "WHERE Clientes.IdentificadorCliente =  @id " +
                    "AND Clientes.Estado = 1";
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
                                (string)lector["NombreDireccionRuta"],
                                (string)lector["NombrePais"],
                                (string)lector["NombreCiudad"] };
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

        /*// POST api/<RutasPorCliente>
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
        }*/
    }
}
