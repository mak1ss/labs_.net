
using Bogus;
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_ServcieManagement.DAL.Seeding
{
    public class CategorySeeder : ISeeder<Category>
    {
        public void Seed(EntityTypeBuilder<Category> builder)
        {
            var faker = new Faker<Category>()
           .RuleFor(c => c.Id, f => 0) 
           .RuleFor(c => c.Name, f => f.Commerce.Department())
           .RuleFor(c => c.Description, f => f.Lorem.Sentence())
           .RuleFor(c => c.CreatedAt, f => f.Date.Recent(2))
           .RuleFor(c => c.UpdatedAt, f => f.Date.Recent(1));

            builder.HasData(faker.Generate(3));
        }
    }
}
