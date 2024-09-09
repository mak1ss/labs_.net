using EF_ServcieManagement.DAL.Configs;
using EF_ServcieManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_ServcieManagement.DAL
{
    public class ServiceManagementContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ServiceManagementContext(DbContextOptions<ServiceManagementContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        }
    }
}
