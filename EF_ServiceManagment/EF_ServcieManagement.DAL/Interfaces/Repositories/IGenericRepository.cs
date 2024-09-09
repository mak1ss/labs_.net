
namespace EF_ServcieManagement.DAL.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity<int>
    {

        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetCompleteEntityAsync(int id);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);
    }
}
