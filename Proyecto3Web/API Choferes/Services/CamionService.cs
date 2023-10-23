using System;
using System.Collections.Generic;
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

        public IEnumerable<string> Get()
        {
            // Aquí deberías realizar una consulta a la base de datos utilizando Dapper.
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<string>("SELECT * FROM dbo.Camiones");
            }
        }

        public IEnumerable<string> GetByNumeroPca(string numeroPlaca)
        {
            // Aquí deberías realizar una consulta a la base de datos utilizando Dapper.
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<string>("SELECT * FROM dbo.Camiones WHERE numeroPlaca = @numeroPlaca", new { numeroPlaca });
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IEnumerable<string> GetByNumeroPlaca(string numeroPlaca)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    string querySQL = "SELECT * FROM dbo.Camiones WHERE numeroPlaca = @numeroPlaca";

                    using (SqlCommand comando = new SqlCommand(querySQL, conexion))
                    {
                        comando.Parameters.AddWithValue("numeroPlaca", numeroPlaca);

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            List<string> returnValues = new List<string>();

                            while (lector.Read())
                            {
                                returnValues.Add((string)lector["Marca"]);
                                returnValues.Add((string)lector["Modelo"]);
                                returnValues.Add(((DateTime)lector["Fabricacion"]).ToString());
                                returnValues.Add((string)lector["Estado"]);
                            }

                            return returnValues;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void UpdateCamion(string numeroPlaca, string Marca, string Modelo, DateTime Fabricacion, string Estado)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void InactivarCamion(string numeroPlaca)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string querySQL =
                        "UPDATE dbo.Camiones SET Estado = 'Inactivo' WHERE numeroPlaca = @numeroPlaca";

                    db.Execute(querySQL, new { numeroPlaca });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
