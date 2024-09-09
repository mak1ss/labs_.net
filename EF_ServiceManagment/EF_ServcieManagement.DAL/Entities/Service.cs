

using EF_ServcieManagement.DAL.Interfaces;

namespace EF_ServcieManagement.DAL.Entities
{
    public class Service : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
