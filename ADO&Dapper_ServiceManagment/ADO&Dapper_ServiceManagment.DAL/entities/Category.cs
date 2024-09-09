using ADO_Dapper_ServiceManagment.DAL.interfaces.entities;

namespace ADO_Dapper_ServiceManagment.DAL.entities
{
    public class Category : IEntity<int>
    {

        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set;}
    }
}
