using ADO_Dapper_ServiceManagment.DAL.interfaces.entities;

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
