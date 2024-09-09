using System.ComponentModel.DataAnnotations;

namespace ADO_Dapper_ServiceManagment.DAL.dtos.serviceTag
{
    public class ServiceTagRequest
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}
