using ADO_Dapper_ServiceManagment.DAL.entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories
{
    public interface IServiceRepository : IGenericRepository<Service, int>
    {

        public IEnumerable<Service> GetServicesByTags(int[] tagIds);
        public IEnumerable<Service> GetServicesByCategory(int categoryId);

    }
}
