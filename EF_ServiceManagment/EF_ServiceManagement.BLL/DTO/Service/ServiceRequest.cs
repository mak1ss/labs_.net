
namespace EF_ServiceManagement.BLL.DTO.Service
{
    public class ServiceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int[] TagIds { get; set; }
    }
}
