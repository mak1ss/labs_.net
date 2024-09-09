using EF_ServcieManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EF_ServcieManagement.DAL.Seeding;

namespace EF_ServcieManagement.DAL.Configs
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(t => t.Id)
                   .UseIdentityColumn()
                   .IsRequired();

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()"); ;

            builder.Property(t => t.UpdatedAt);


            new TagSeeder().Seed(builder);
        }
    }
}
