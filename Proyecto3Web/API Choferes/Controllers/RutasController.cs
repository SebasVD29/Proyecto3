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

                this.conexion.Open();

                string querySQL = "SELECT ruta.Nombre as NombreRuta," +
                    "pais.Nombre as NombrePais," +
                    "ciudad.nombre as NombreCiudad, " +
                    "ruta.Descripcion, ruta.FechaInicio, ruta.FechaFinal, chofer.IdentificadorChofer," +
                    " Chofer.Nombre as NombreChofer,chofer.Apellido, Camiones.numeroPlaca, ruta.EstadoEntrega\r\n" +
                    "FROM [dbo].[Ruta]\r\n" +
                    "INNER JOIN DireccionRuta ON [dbo].[Ruta].IdDireccionRuta = DireccionRuta.IdDireccionRuta\r\n" +
                    "INNER JOIN Chofer ON [dbo].[Ruta].IdChofer = Chofer.IdentificadorChofer\r\n" +
                    "INNER JOIN Camiones ON [dbo].[Ruta].numeroPlaca = Camiones.numeroPlaca\r\n" +
                    "INNER JOIN PaisCiudad ON DireccionRuta.idPaisCiudad = PaisCiudad.idPaisCiudad\r\n" +
                    "INNER JOIN Ciudad ON  Ciudad.IdentificadorCiudad = PaisCiudad.IdCiudad \r\n" +
                    "INNER JOIN Pais ON Pais.IdentificadorPais = PaisCiudad.IdPais\r\n" +
                    "WHERE IdRuta = @id";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                           
                            return new string[] { 
                                (string)lector["NombreRuta"], 
                                (string)lector["NombrePais"], 
                                (string)lector["NombreCiudad"], 
                                (string)lector["Descripcion"], 
                                (string)lector["IdentificadorChofer"].ToString(), 
                                (string)lector["NombreChofer"], 
                                (string)lector["Apellido"], 
                                (string)lector["numeroPlaca"].ToString(), 
                                (string)lector["EstadoEntrega"].ToString(), 
                                (string)lector["FechaInicio"].ToString(), 
                                (string)lector["FechaFinal"].ToString() };

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
        public void Put(int id, string descripcion, int idChofer, string placa, int estado, string inicio, string final)
        {
            try
            {
                this.conexion.Open();
                string querySQL =
                    "UPDATE [dbo].[Ruta]\r\nSET Ruta.Descripcion = @descripcion , " +
                    "Ruta.IdChofer = @idChofer, " +
                    "Ruta.numeroPlaca = @placa, " +
                    "Ruta.EstadoEntrega = @estado, " +
                    "Ruta.FechaInicio = @inicio, " +
                    "Ruta.FechaFinal = @final" +
                    "\r\nFROM [dbo].[Ruta]\r\n" +
                    "INNER JOIN DireccionRuta ON [dbo].[Ruta].IdDireccionRuta = DireccionRuta.IdDireccionRuta\r\n" +
                    "INNER JOIN Chofer ON [dbo].[Ruta].IdChofer = Chofer.IdentificadorChofer\r\n" +
                    "INNER JOIN Camiones ON [dbo].[Ruta].numeroPlaca = Camiones.numeroPlaca\r\n" +
                    "INNER JOIN PaisCiudad ON DireccionRuta.idPaisCiudad = PaisCiudad.idPaisCiudad\r\n" +
                    "INNER JOIN Ciudad ON  Ciudad.IdentificadorCiudad = PaisCiudad.IdCiudad \r\n" +
                    "INNER JOIN Pais ON Pais.IdentificadorPais = PaisCiudad.IdPais\r\n" +
                    "WHERE IdRuta = @id";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("idChofer", idChofer);
                    comando.Parameters.AddWithValue("descripcion", descripcion);
                    comando.Parameters.AddWithValue("placa", placa);
                    comando.Parameters.AddWithValue("estado", estado);
                    comando.Parameters.AddWithValue("inicio", inicio);
                    comando.Parameters.AddWithValue("final", final);
                    comando.ExecuteNonQuery();

                }
                this.conexion.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return;
        }

        // DELETE api/<RutasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
