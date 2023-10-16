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
    public class adminsControllers : ControllerBase
    {
        string stringEncriptada = "";
        string stringDesencriptada = "";
        private securityController securityController;
        private DataBaseController dataBase;
        private SqlConnection conexion;


        int count = 0;

        public adminsControllers()
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
                string querySQL = "Select * from dbo. where ";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion ))
                {
                   //comando.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            //return ;

                        }

                        lector.Close();
                    }
                    this.conexion.Close();
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
        public void Post()
        {
    
            //stringEncriptada = securityController.EncriptarBase64(contrasena);
            

            try
            {
                this.conexion.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "INSERT INTO dbo.() " +
                    "VALUES ()";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    

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
        public void Put()
        {
            

            try
            {
                //stringEncriptada = securityController.EncriptarBase64(contrasena);
                this.conexion.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "UPDATE dbo. SET   " +
                    "WHERE  " ;

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    
                    
                    comando.ExecuteNonQuery();


                   
                }
                this.conexion.Close();

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
