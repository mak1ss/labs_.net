using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace EF_ServcieManagement.DAL
{
    public class ServiceManagementContextFactory : IDesignTimeDbContextFactory<ServiceManagementContext>
    {
        public ServiceManagementContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EF_ServiceManagement.WEBAPI"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ServiceManagementContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new(optionsBuilder.Options);
        }
    }
}
