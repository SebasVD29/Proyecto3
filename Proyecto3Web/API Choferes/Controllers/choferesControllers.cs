 using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Choferes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class choferesControllers : ControllerBase
    {
        string stringEncriptada = "";
        string stringDesencriptada = "";
        private securityController securityController;
        private DataBaseController dataBase ;
        private SqlConnection conexion;


        int count = 0;

        public choferesControllers()
        { 
            this.dataBase = new DataBaseController();
            this.securityController = new securityController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }

        // GET: api/<choferesControllers>
        [HttpGet]
        public string[][] Get()
        {
            try
            {
                this.conexion.Open();

                // Cantidad de rutas 
                string querySQL = "Select * from dbo.Chofer";
                int cantidadChoferes = 0;
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            cantidadChoferes++;
                        }
                        lector.Close();
                    }

                }
                string[][] returnValues = new string[cantidadChoferes][];
                int contador = 0;
                querySQL = "Select * from dbo.Chofer";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            // Devolver array en lugar de primer elemento 
                            returnValues[contador] = new string[] { 
                                ((int)lector["IdentificadorChofer"]).ToString(), 
                                (string)lector["Nombre"], 
                                (string)lector["Apellido"] };

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

        // GET api/<choferesControllers>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string estado = "";
            try
            {
                this.conexion.Open();

                string querySQL = "Select * from dbo.Chofer where IdentificadorChofer = @id";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {

                            if ((int)lector["Estado"] == 1)
                            {
                                estado = "Activo";
                            }
                            else
                            {
                                estado = "Inactivo";
                            }
                        }

                        return Ok(new string[]
                        {
                            (string)lector["Nombre"],
                            (string)lector["Apellido"],
                            (string)lector["Email"],
                            estado});
                        

                        lector.Close();
                    }

                }

                this.conexion.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en la BD del select. {sqlEx.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = sqlEx.Message });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general del select. {ex.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = ex.Message });
            }
        }

            
        // POST api/<choferesControllers>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public IActionResult Post(int identificacion, string nombre, string apellidos, string email, string contrasena, int estado)
        {
            DateTime fecha = DateTime.Now;
            stringEncriptada = this.securityController.Encriptar(contrasena);
            try
            {
                this.conexion.Open();
                string querySQL =
                    "INSERT INTO dbo.Chofer(IdentificadorChofer, Nombre, Apellido, Email, Contraseña, FechaRegistro, Estado) " +
                    "VALUES (@identificador, @nombre, @apellidos, @email, @password, @fecha, @estado)";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    comando.Parameters.AddWithValue("identificador", identificacion);
                    comando.Parameters.AddWithValue("nombre", nombre);
                    comando.Parameters.AddWithValue("apellidos", apellidos);
                    comando.Parameters.AddWithValue("email", email);
                    comando.Parameters.AddWithValue("password", stringEncriptada);
                    comando.Parameters.AddWithValue("fecha", fecha);
                    comando.Parameters.AddWithValue("estado", estado);
                    comando.ExecuteNonQuery();

                }
                this.conexion.Close();
                return Ok(new { Message = "Operación exitosa" });
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en la BD del insert. {sqlEx.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = sqlEx.Message });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general del insert. {ex.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = ex.Message });
            }
        }

        // PUT api/<choferesControllers>/5
        [HttpPut("{id}")]
        public IActionResult Put(int identificacion, string nombre, string apellidos, string email, string contrasena, int estado)
        {
            try
            {
                stringEncriptada = this.securityController.Encriptar(contrasena);
                this.conexion.Open();
                string querySQL;

                if (contrasena == "NoCambiarContrasena")
                {
                    querySQL =
                    "UPDATE dbo.Chofer SET  Nombre = @nombre, Apellido = @apellidos, Email = @email, Estado = @estado " +
                    "WHERE  IdentificadorChofer = @id";
                }
                else {
                    querySQL =
                       "UPDATE dbo.Chofer SET  Nombre = @nombre, Apellido = @apellidos, Email = @email, Contraseña = @password, Estado = @estado " +
                       "WHERE  IdentificadorChofer = @id";
                }
               

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    comando.Parameters.AddWithValue("@id", identificacion);

                    comando.Parameters.AddWithValue("nombre", nombre);
                    comando.Parameters.AddWithValue("apellidos", apellidos);
                    comando.Parameters.AddWithValue("email", email);
                    comando.Parameters.AddWithValue("password", stringEncriptada);
                    comando.Parameters.AddWithValue("estado", estado);
                    comando.ExecuteNonQuery();

                }
                this.conexion.Close();
                return Ok(new { Message = "Operación exitosa" });
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en la BD del update. {sqlEx.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = sqlEx.Message });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general del update. {ex.Message}");
                return StatusCode(500, new { Error = "Error inesperado", Message = ex.Message });
            }
        }
    }
}

