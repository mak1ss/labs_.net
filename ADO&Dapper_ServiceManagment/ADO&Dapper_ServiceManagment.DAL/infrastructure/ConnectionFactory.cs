using ADO_Dapper_ServiceManagment.DAL.interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        private static string connectionString;

        public ConnectionFactory(IConfiguration config)
        {
            _configuration = config;
        }
        public void SetConnection(string connectionString)
        {
            ConnectionFactory.connectionString = connectionString;
        }

        public IDbConnection GetSqlConnection
        {
            get
            {
                SqlConnection connection;

                if (!string.IsNullOrEmpty(connectionString))
                    connection = new SqlConnection(connectionString);
                else
                    connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                connection.Open();

                return connection;
            }
        }
    }
}
