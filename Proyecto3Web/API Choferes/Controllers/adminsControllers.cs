using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

namespace API_Choferes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class AdminsControllers : ControllerBase
    {
        string stringEncriptada = "";
       
        private securityController securityController;
        private DataBaseController dataBase;
        private SqlConnection conexion;


        int count = 0;

        public AdminsControllers()
        {
            this.dataBase = new DataBaseController();
            this.securityController = new securityController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }
  
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string estado = "";
            try
            {
                this.conexion.Open();
                string querySQL = "Select * from dbo.Administrador where IdentificadorAdministrador = @id";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion ))
                {
                   comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
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
                            estado
                        });


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
                    "INSERT INTO dbo.Administrador(IdentificadorAdministrador, Nombre, Apellido, Email, Contraseña, FechaRegistro, Estado) " +
                    "VALUES (@identificador, @nombre, @apellido, @email, @password, @fecha, @estado)";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("identificador", identificacion);
                    comando.Parameters.AddWithValue("nombre", nombre);
                    comando.Parameters.AddWithValue("apellido", apellidos);
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
                    "UPDATE dbo.Administrador SET Nombre = @nombre, Apellido = @apellido, Email = @email, Estado = @estado  " +
                    "WHERE IdentificadorAdministrador = @id ";
                }
                else
                {
                    querySQL =
                       "UPDATE dbo.Administrador SET Nombre = @nombre, Apellido = @apellido, Email = @email, Contraseña = @password , Estado = @estado  " +
                    "WHERE IdentificadorAdministrador = @id ";
                }
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("id", identificacion);

                    comando.Parameters.AddWithValue("nombre", nombre);
                    comando.Parameters.AddWithValue("apellido", apellidos);
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

        [HttpPost("login")]
        public IActionResult Login(string correo, string password)
        {
                         
            try
            {
                this.conexion.Open();
                string querySQL = "Select * from dbo.Administrador where Email = @email AND Estado = 1 ";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    comando.Parameters.AddWithValue("@email", correo);
                    
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while(lector.Read())
                        {
                            var email = (string)lector["Email"];
                            string pass = this.securityController.Desencriptar((string)lector["Contraseña"]);
                          
                            if (email.Equals(correo) && pass.Equals(password)) 
                            { 
                                Console.WriteLine("Hizo el if");
                                var data = new { success = true, message = "Autenticación exitosa" };
                                return Ok(data);
                
                            }
                           
                        }

                        lector.Close();
                    }
                }
                this.conexion.Close();
                Console.WriteLine($"Error del login.");
                var errorData = new { success = false, message = "Autenticación fallida" };
                return BadRequest(errorData);

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en la BD del login. {sqlEx.Message}");
                var errorData = new { success = false, message = "Error interno: " + sqlEx.Message };
                return StatusCode(500, errorData);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general del login. {ex.Message}");
                var errorData = new { success = false, message = "Error interno: " + ex.Message };
                return StatusCode(500, errorData);
            }
        }

    }
}
