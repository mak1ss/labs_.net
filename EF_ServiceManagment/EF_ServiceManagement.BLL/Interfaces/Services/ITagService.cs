using EF_ServiceManagement.BLL.DTO.Tag;

namespace EF_ServiceManagement.BLL.Interfaces.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponse>> GetAsync();

        Task<TagResponse> GetByIdAsync(int id);

        Task InsertAsync(TagRequest request);

        Task UpdateAsync(TagRequest request);

        Task DeleteAsync(int id);
    }
}
