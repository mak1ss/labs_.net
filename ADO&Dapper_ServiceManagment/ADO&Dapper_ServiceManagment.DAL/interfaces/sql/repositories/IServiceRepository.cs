using ADO_Dapper_ServiceManagment.DAL.entities;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories
{
    public interface IServiceRepository : IGenericRepository<Service, int>
    {

        public IEnumerable<Service> GetServicesByTagNames(string[] tagNames);
        public IEnumerable<Service> GetServicesByCategory(int categoryId);

    }
}
