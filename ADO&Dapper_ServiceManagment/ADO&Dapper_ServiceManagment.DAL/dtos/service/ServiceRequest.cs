using System.ComponentModel.DataAnnotations;

namespace ADO_Dapper_ServiceManagment.DAL.dtos.service
{
    public class ServiceRequest
    {
        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
