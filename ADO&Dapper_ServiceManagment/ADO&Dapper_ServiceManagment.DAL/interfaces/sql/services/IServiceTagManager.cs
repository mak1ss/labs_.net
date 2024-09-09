using ADO_Dapper_ServiceManagment.DAL.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services
{
    public interface IServiceTagManager
    {

        long CreateServiceTag(ServiceTag entity);

        ServiceTag GetServiceTag(int id);

        void UpdateServiceTag(ServiceTag entity);

        void DeleteServiceTag(ServiceTag entity);

        IEnumerable<ServiceTag> GetAllServiceTags();
    }
}
