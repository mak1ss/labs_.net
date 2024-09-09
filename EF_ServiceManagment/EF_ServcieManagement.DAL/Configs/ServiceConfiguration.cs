using EF_ServcieManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EF_ServcieManagement.DAL.Seeding;


namespace EF_ServcieManagement.DAL.Configs
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(s => s.Id)
                    .UseIdentityColumn()
                    .IsRequired();

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Description)
                   .HasMaxLength(500);

            builder.Property(s => s.Price)
                   .IsRequired()
                   .HasColumnType("decimal(10, 2)");

            builder.Property(s => s.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.UpdatedAt);

            builder.HasOne(s => s.Category)
                   .WithMany(c => c.Services)
                   .HasForeignKey(s => s.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Tags)
               .WithMany(t => t.Services)
               .UsingEntity(j => j.ToTable("serviceTag"));

            new ServiceSeeder().Seed(builder);
        }
    }
}
