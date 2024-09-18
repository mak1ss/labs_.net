
using Bogus;
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_ServcieManagement.DAL.Seeding
{
    public class CategorySeeder : ISeeder<Category>
    {
        public void Seed(EntityTypeBuilder<Category> builder)
        {
            int id = 1;
            var faker = new Faker<Category>()
             .RuleFor(s => s.Id, f => id++)
              .RuleFor(c => c.Name, f => f.Commerce.Department())
               .RuleFor(c => c.Description, f => f.Lorem.Sentence())
               .RuleFor(c => c.CreatedAt, f => f.Date.Recent(2))
                .RuleFor(c => c.UpdatedAt, f => f.Date.Recent(1));

            builder.HasData(faker.Generate(3));
        }
    }
}
