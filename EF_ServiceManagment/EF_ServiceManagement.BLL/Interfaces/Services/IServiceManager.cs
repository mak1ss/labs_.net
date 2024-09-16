using EF_ServiceManagement.BLL.DTO.Service;

namespace EF_ServiceManagement.BLL.Interfaces.Services
{
    public interface IServiceManager
    {
        Task<IEnumerable<ServiceResponse>> GetAsync();

        Task<ServiceResponse> GetByIdAsync(int id);

        Task InsertAsync(ServiceRequest request);

        Task UpdateAsync(ServiceRequest request);

        Task DeleteAsync(int id);

        Task<IEnumerable<ServiceResponse>> GetServicesByCategoryAsync(int categoryId);

        Task<IEnumerable<ServiceResponse>> GetServicesByTagsAsync(string[] tagNames);

        Task<ServiceResponse> AddTagAsync(int tagId, int serviceId);

        Task<ServiceResponse> DeleteTagAsync(int tagId, int serviceId);
    }
}
