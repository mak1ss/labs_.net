using EF_ServcieManagement.DAL.Entities;
using EF_ServiceManagement.BLL.DTO.Service;

namespace EF_ServiceManagement.BLL.Interfaces.Services
{
    public interface IServiceManager
    {
        Task<IEnumerable<ServiceResponse>> GetAsync();

        Task<IEnumerable<ServiceResponse>> GetByIdAsync();

        Task InsertAsync(ServiceRequest request);

        Task UpdateAsync(ServiceRequest request);

        Task DeleteAsync(int id);

        Task<IEnumerable<Service>> GetServicesByCategoryAsync(int categoryId);

        Task<IEnumerable<Service>> GetServicesByTagsAsync(string[] tagNames);

        Task<Service> AddTagAsync(int tagId, int serviceId);

        Task<Service> DeleteTagAsync(int tagId, int serviceId);
    }
}
