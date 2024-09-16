using ADO_Dapper_ServiceManagment.DAL.interfaces.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces
{
    public interface IGenericRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(TId Id);

        long Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
