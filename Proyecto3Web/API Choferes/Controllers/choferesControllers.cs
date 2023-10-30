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
        public IEnumerable<string> Get()
        {         
            return new string[] { "value1", "value2" };
        }

        // GET api/<choferesControllers>/5
        [HttpGet("{id}")]
        public string[] Get(int id)
        {

            try
            {
                string[] returnValues = new string[100];
                //int counter = 0;
           
                this.conexion.Open();

                string querySQL = "Select * from dbo.Chofer where IdentificadorChofer = @id";
                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {

                    comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader()) 
                    {
                        while (lector.Read())
                        {
                            string estado = "";
                            if ((int)lector["Estado"] == 1)
                            {
                                estado = "Activo";
                            }
                            else
                            {
                                estado = "Inactivo";
                            }
                            return new string[] { (string)lector["Nombre"], (string)lector["Apellido"], (string)lector["Email"], estado};

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

            
        // POST api/<choferesControllers>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void Post(int identificacion, string nombre, string apellidos, string email, string contrasena, int estado)
        {
            DateTime fecha = DateTime.Now;
            stringEncriptada = this.securityController.Encriptar(contrasena);
            /*
            if (estado == "Activo") estado = "1";
            if (estado == "Inactivo") estado = "0";
            */
            try
            {
                this.conexion.Open();
                string[] returnValues = new string[100];
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
        public void Put(int identificacion, string nombre, string apellidos, string email, string contrasena, int estado)
        {
            

            try
            {

                stringEncriptada = this.securityController.Encriptar(contrasena);
                this.conexion.Open();
                string[] returnValues = new string[100];
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
                /*
                if (estado == "Activo") estado = "1";
                if (estado == "Inactivo") estado = "0";*/

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

