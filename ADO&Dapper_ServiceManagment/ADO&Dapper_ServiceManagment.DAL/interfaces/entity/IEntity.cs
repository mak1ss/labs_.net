using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces.entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
