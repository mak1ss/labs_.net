using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces
{
    public interface IUnitOfWork
    {

        ICategoryRepository CategoryRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IServiceTagRepository ServiceTagRepository { get; }
        ITagRepository TagRepository { get; }

        void Complete();
    }
}
