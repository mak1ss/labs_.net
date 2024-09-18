using EF_ServcieManagement.DAL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_ServcieManagement.DAL.Entities
{
    [Table("categories")]
    public class Category : IEntity<int>, ITrackable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
