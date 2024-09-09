using System.ComponentModel.DataAnnotations;

namespace ADO_Dapper_ServiceManagment.DAL.dtos.tag
{
    public class TagRequest
    {
        [Required]
        [MaxLength(50)]
        public string TagName { get; set; }
    }
}
