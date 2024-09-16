
using EF_ServiceManagement.BLL.DTO.Category;
using EF_ServiceManagement.BLL.DTO.Tag;

namespace EF_ServiceManagement.BLL.DTO.Service
{
    public class ServiceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse Category { get; set; }
        public ICollection<TagResponse> Tags { get; set; } = new List<TagResponse>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
