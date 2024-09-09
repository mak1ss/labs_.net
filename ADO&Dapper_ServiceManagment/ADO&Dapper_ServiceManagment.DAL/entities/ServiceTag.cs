using ADO_Dapper_ServiceManagment.DAL.interfaces.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.entities
{
    public class ServiceTag : IEntity<int>
    {

        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int TagId { get; set; }
    }
}
