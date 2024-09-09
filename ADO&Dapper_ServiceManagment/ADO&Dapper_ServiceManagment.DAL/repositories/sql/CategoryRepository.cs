using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories;
using Microsoft.Extensions.Configuration;

namespace ADO_Dapper_ServiceManagment.DAL.repositories.sql
{
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {
        private static readonly string tableName = "Category";
        public CategoryRepository(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, tableName)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            connectionFactory.SetConnection(connectionString);
        }
    }
}
