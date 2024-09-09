using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.dtos.serviceTag
{
    public class ServiceTagResponse
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int TagId { get; set; }
    }
}
