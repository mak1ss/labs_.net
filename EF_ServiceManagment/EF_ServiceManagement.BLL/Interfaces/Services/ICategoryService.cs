
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces;
using EF_ServiceManagement.BLL.DTO.Category;

namespace EF_ServiceManagement.BLL.Interfaces.Services
{
    public interface ICategoryService
    {

        Task<IEnumerable<CategoryResponse>> GetAsync();

        Task<IEnumerable<CategoryResponse>> GetByIdAsync();
        
        Task InsertAsync(CategoryRequest request);

        Task UpdateAsync(CategoryRequest request);

        Task DeleteAsync(int id);
    }
}
