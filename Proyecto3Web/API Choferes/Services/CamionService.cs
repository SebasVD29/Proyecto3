using System.Data;
using System.Data.SqlClient;
using API_Choferes.Models;
using Dapper;

namespace API_Choferes.Services
{
    public class CamionesService
    {
        private readonly string connectionString;

        public CamionesService(string connectionString)
        {
            this.connectionString = connectionString;
        }

       /* public IEnumerable<string> Get()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<string>("SELECT * FROM dbo.Camiones");
            }
        }
        */
        public IEnumerable<CamionModel> GetByNumeroPlaca(string numeroPlaca)
        {
            try 
            { 
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<CamionModel>("SELECT * FROM dbo.Camiones WHERE numeroPlaca = @numeroPlaca", new { numeroPlaca });
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en el servicio de select {sqlEx.Message}");
                throw;

            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error en el servicio del select {ex.Message}");
                throw;
            }
        }


        public void InsertCamion(CamionModel camion)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string querySQL =
                        "INSERT INTO dbo.Camiones(numeroPlaca, Marca, Modelo, Fabricacion, Estado) " +
                        "VALUES (@numeroPlaca, @Marca, @Modelo, @Fabricacion, @Estado)";

                    db.Execute(querySQL, camion);
                }
                Console.WriteLine($"Camión agregado correctamente: {camion.Marca} {camion.Modelo} (Placa: {camion.numeroPlaca})");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en el servicio de insert {sqlEx.Message}");
                throw;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el servicio de Insert {ex.Message}");
                throw;
            }
        }

        public void UpdateCamion(string numeroPlaca, string Marca, string Modelo, int Fabricacion, int Estado)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string querySQL =
                        "UPDATE dbo.Camiones SET  Marca = @Marca, Modelo = @Modelo,Fabricacion = @Fabricacion, Estado = @Estado " +
                        "WHERE  numeroPlaca = @numeroPlaca";

                    db.Execute(querySQL, new { numeroPlaca, Marca, Modelo, Fabricacion, Estado });
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error en el servicio de update {sqlEx.Message}");
                throw;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Console.WriteLine($"Error en el servicio de Update {ex.Message}");
                throw;

            }
        }

    }
}

