using ADO_Dapper_ServiceManagment.DAL.interfaces.entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.entities
{
    public class Tag : IEntity<int>
    {
        public int Id {  get; set; }

        public string TagName {  get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
