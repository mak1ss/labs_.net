using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.repositories.sql
{
    public class ServiceRepostiory : GenericRepository<Service, int>, IServiceRepository
    {
        private static readonly string tableName = "Service";
        public ServiceRepostiory(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, tableName)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            connectionFactory.SetConnection(connectionString);
        }

        public IEnumerable<Service> GetServicesByTags(int[] tagIds)
        {
            var query = "SP_GetServicesByTagIds";
            string tagIdsString = String.Join(",", tagIds);
            using (var db = _connectionFactory.GetSqlConnection)
            {
                return db.Query<Service>(query,
                    new { tagIdsString },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Service> GetServicesByCategory(int categoryId)
        {
            var query = "SL_GetServicesByCategory";

            using (var db = _connectionFactory.GetSqlConnection)
            {
                return db.Query<Service>(
                    query,
                    new { categoryId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
