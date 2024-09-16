using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADO_Dapper_ServiceManagment.DAL.repositories.sql
{
    public class ServiceRepostiory : GenericRepository<Service, int>, IServiceRepository
    {
        private static readonly string tableName = "Service";
        private readonly string connectionString;
        public ServiceRepostiory(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, tableName)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Service> GetServicesByTagNames(string[] tagNames)
        {
            var services = new List<Service>();
            string tagNamesString = String.Join(",", tagNames.Select(t => $"'{t}'"));
            string query = $@"
                       SELECT s.*
                       FROM Service s
                       INNER JOIN ServiceTag st ON s.Id = st.ServiceId
                       INNER JOIN Tag t ON st.TagId = t.Id
                       WHERE t.TagName IN ({tagNamesString})
                       GROUP BY s.Id, s.ServiceName, s.Price, s.Description, s.CategoryId, s.CreatedAt, s.UpdatedAt;";

            using (var dbConnection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, dbConnection))
            {
                dbConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var service = new Service
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ServiceName = reader["ServiceName"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Description = reader["Description"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdatedAt"]) : null
                        };

                        services.Add(service);
                    }
                }
            }

            return services;
        }


        public IEnumerable<Service> GetServicesByCategory(int categoryId)
        {
            var services = new List<Service>();
            var query = "SP_GetServicesByCategoryId";

            using (var dbConnection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, dbConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@P_categoryId", categoryId);

                dbConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var service = new Service
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ServiceName = reader["ServiceName"].ToString(),
                            Description = reader["Description"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdatedAt"]) : null
                        };

                        services.Add(service);
                    }
                }
            }

            return services;
        }
    }
}
