using API_Choferes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Camiones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class camionesControllers : ControllerBase
    {
       
        private DataBaseController dataBase;
        private SqlConnection conexion;


        int count = 0;

        public camionesControllers()
        {
            this.dataBase = new DataBaseController();
            this.conexion = new SqlConnection(this.dataBase.StringConexion());
        }
        // GET: api/<camionesControllers>
        [HttpGet]
        public IEnumerable<string> Get()
        {         
            return new string[] { "value1", "value2" };
        }

        // GET api/<camionesControllers>/5
        [HttpGet("{NumeroPlaca}")]
        public string[] Get(int numeroPlaca)
        {
            
            try
            {
                this.conexion.Open();
                string[] returnValues = new string[100];
                //int counter = 0;
                string querySQL = "Select * from dbo.Camiones where numerPlaca = " + numeroPlaca;

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            return new string[] { (string)lector["Marca"], (string)lector["Modelo"] };

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

        // POST api/<camionesControllers>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void Post(int numeroPlaca, string Marca, string Modelo, string AñoFabricacion, string Estado)
        {
            DateTime fecha = DateTime.Now;
           
            try
            {
                this.conexion.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "INSERT INTO dbo.Camiones(numeroPlaca, Marca, Modelo, AñoFabricacion, Estado) " +
                    "VALUES (@numeroPlaca, @Marca, @Modelo, @AnoFabricacion, @Estado)";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
             
                    comando.Parameters.AddWithValue("numeroPlaca", numeroPlaca);
                    comando.Parameters.AddWithValue("Marca", Marca);
                    comando.Parameters.AddWithValue("Modelo", Modelo);
                    comando.Parameters.AddWithValue("AnoFabricacion", AñoFabricacion);
                    comando.Parameters.AddWithValue("Estado", Estado);
                    comando.ExecuteNonQuery();


                    /*SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    { }
                    reader.Close();*/
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

        // PUT api/<camionesControllers>/5
        [HttpPut("{numeroPlaca}")]
        public void Put(int numeroPlaca, string Marca, string Modelo, string AñoFabricacion,  string Estado)
        {
           
            try
            {

                this.conexion.Open();
                string[] returnValues = new string[100];
                string querySQL =
                    "UPDATE dbo.Camiones SET  Marca = @Marca, Modelo = @Modelo, Estado = @Estado " +
                    "WHERE  numeroPlaca = " + numeroPlaca;

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    
                    comando.Parameters.AddWithValue("numeroPlaca", numeroPlaca);
                    comando.Parameters.AddWithValue("Marca", Marca);
                    comando.Parameters.AddWithValue("Modelo", Modelo);
                    comando.Parameters.AddWithValue("Estado", Estado);
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

        // DELETE api/<camionesControllers>/5
        [HttpDelete("{numeroPlaca}")]
        public void Delete(int numeroPlaca)
        {
        }
    }
}
