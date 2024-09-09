using Bogus;
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_ServcieManagement.DAL.Seeding
{
    public class TagSeeder : ISeeder<Tag>
    {
        public void Seed(EntityTypeBuilder<Tag> builder)
        {
            var faker = new Faker<Tag>()
                .RuleFor(t => t.Id, f => 0)  // Id буде генеруватись базою
                .RuleFor(t => t.Name, f => f.Commerce.ProductAdjective())
                .RuleFor(c => c.CreatedAt, f => f.Date.Recent(2))
                .RuleFor(c => c.UpdatedAt, f => f.Date.Recent(1));

            builder.HasData(faker.Generate(5));
        }
    }
}
