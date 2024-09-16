using EF_ServiceManagement.BLL.DTO.Category;

namespace EF_ServiceManagement.BLL.Interfaces.Services
{
    public interface ICategoryService
    {

        Task<IEnumerable<CategoryResponse>> GetAsync();

        Task<CategoryResponse> GetByIdAsync(int id);
        
        Task InsertAsync(CategoryRequest request);

        Task UpdateAsync(CategoryRequest request);

        Task DeleteAsync(int id);
    }
}
