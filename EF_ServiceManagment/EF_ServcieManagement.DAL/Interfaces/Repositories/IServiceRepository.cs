
using EF_ServcieManagement.DAL.Entities;

namespace EF_ServcieManagement.DAL.Interfaces.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<IEnumerable<Service>> GetServicesByCategoryAsync(int categoryId);

        Task<IEnumerable<Service>> GetServicesByTagsAsync(string[] tagNames);

        Task<Service> AddTagAsync(Tag tag, int serviceId);

        Task<Service> DeleteTagAsync(int tagId, int serviceId);
        
    }
}
