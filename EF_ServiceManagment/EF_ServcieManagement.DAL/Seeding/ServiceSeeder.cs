using EF_ServcieManagement.DAL.Interfaces;
using EF_ServcieManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace EF_ServcieManagement.DAL.Seeding
{
    public class ServiceSeeder : ISeeder<Service>
    {
        public void Seed(EntityTypeBuilder<Service> builder)
        {
            var random = new Random();
            int id = 1;
            var faker = new Faker<Service>()
                .RuleFor(s => s.Id, f => id++)
                .RuleFor(s => s.Name, f => f.Commerce.ProductName())
                .RuleFor(s => s.Description, f => f.Lorem.Sentences(3))
                .RuleFor(s => s.Price, f => f.Finance.Amount(50, 1000))
                .RuleFor(c => c.CreatedAt, f => f.Date.Recent(2))
                .RuleFor(c => c.UpdatedAt, f => f.Date.Recent(1))
                .RuleFor(s => s.CategoryId, f => random.Next(4));

            builder.HasData(faker.Generate(10));
        }
    }
}
