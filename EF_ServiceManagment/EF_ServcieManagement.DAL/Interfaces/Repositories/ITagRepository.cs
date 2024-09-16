using EF_ServcieManagement.DAL.Entities;

namespace EF_ServcieManagement.DAL.Interfaces.Repositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetTagsByIds(int[] ids);
    }
}
