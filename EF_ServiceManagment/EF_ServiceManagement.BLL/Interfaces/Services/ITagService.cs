
using EF_ServiceManagement.BLL.DTO.Category;
using EF_ServiceManagement.BLL.DTO.Tag;

namespace EF_ServiceManagement.BLL.Interfaces.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponse>> GetAsync();

        Task<IEnumerable<TagResponse>> GetByIdAsync();

        Task InsertAsync(TagRequest request);

        Task UpdateAsync(TagRequest request);

        Task DeleteAsync(int id);
    }
}
