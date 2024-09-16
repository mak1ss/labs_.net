using ADO_Dapper_ServiceManagment.DAL.interfaces.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.entities
{
    public class Service : IEntity<int>
    {

        public int Id { get; set; }
        
        public string ServiceName { get; set; }

        public string Description { get; set; }

        public int CategoryId {  get; set; }

        public double Price { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
