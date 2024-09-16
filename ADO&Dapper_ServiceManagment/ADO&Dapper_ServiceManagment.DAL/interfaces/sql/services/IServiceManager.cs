using ADO_Dapper_ServiceManagment.DAL.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services
{
    public interface IServiceManager
    {

        long CreateService(Service entity);

        Service GetServiceById(int id);

        void UpdateService(Service entity);

        void DeleteService(Service entity);
        
        IEnumerable<Service> GetAllServices();

        IEnumerable<Service> GetServicesByTags(string[] tagNames);

        IEnumerable<Service> GetServicesByCategory(int category);
    }
}
