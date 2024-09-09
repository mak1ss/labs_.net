using System.ComponentModel.DataAnnotations;

namespace ADO_Dapper_ServiceManagment.DAL.dtos.category
{
    public class CategoryRequest
    {
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
