using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.repositories.sql
{
    public class ServiceTagRepository : GenericRepository<ServiceTag, int>, IServiceTagRepository
    {

        private static readonly string tableName = "ServiceTag";

        public ServiceTagRepository(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, tableName)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            connectionFactory.SetConnection(connectionString);
        }
    }
}
