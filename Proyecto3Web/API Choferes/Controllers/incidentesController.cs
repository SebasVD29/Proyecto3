
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Incidentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class incidentesControllers : ControllerBase
    {

        private DataBaseController dataBase;
        private SqlConnection conexion;


        // GET api/<choferesControllers>/5
        [HttpGet("{id}")]
        public List<string[]> Get(int id, DateTime fechaInicio, DateTime fechaFinal)
        {
            List<string[]> incidencias = new List<string[]>();

            try
            {
                this.conexion.Open();

                string querySQL = "SELECT * FROM dbo.Incidentes WHERE IdentificadorRuta = @id AND Fecha BETWEEN @fechaInicio AND @fechaFinal";

                using (SqlCommand comando = new SqlCommand(querySQL, this.conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@fechaFinal", fechaFinal);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string identificadorIncidente = Convert.ToString(lector["IdentificadorIncidente"]);
                            string fecha = Convert.ToString(lector["Fecha"]);
                            string solucion = Convert.ToString(lector["Solucion"]);

                            incidencias.Add(new string[] { identificadorIncidente, fecha, solucion });
                        }

                        lector.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                incidencias.Clear();
                incidencias.Add(new string[] { "error", "error" });
            }
            finally
            {
                this.conexion.Close();
            }

            return incidencias;
        }





        // DELETE api/<choferesControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

