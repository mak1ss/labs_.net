using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using EF_ServcieManagement.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EF_ServcieManagement.DAL.Data.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ServiceManagementContext context) : base(context) { }

        public override async Task<Service> GetCompleteEntityAsync(int id)
        {
            var service = await table.Include(s => s.Category)
                                     .Include(s => s.Tags)
                                     .SingleOrDefaultAsync(s => s.Id == id);

            return service ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }

        public async Task<Service> AddTagAsync(Tag tag, int serviceId)
        {
            var service = await table.Include(s => s.Tags)
                                     .SingleOrDefaultAsync(s => s.Id == serviceId);

            if (service == null) throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(serviceId));

            if (!service.Tags.Any(t => t.Id == tag.Id))
            {
                service.Tags.Add(tag);
            }
            else
            {
                throw new InvalidOperationException("Tag already associated with this service");
            }

            return service;
        }

        public async Task<Service> DeleteTagAsync(int tagId, int serviceId)
        {
            var service = await table.Include(s => s.Tags)
                                     .SingleOrDefaultAsync(s => s.Id == serviceId);

            if (service == null) throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(serviceId));

            var tag = service.Tags.FirstOrDefault(t => t.Id == tagId);

            if (tag == null)
            {
                throw new EntityNotFoundException($"Tag with id {tagId} is not associated with this service");
            }

            service.Tags.Remove(tag);

            return service;
        }

        public async Task<IEnumerable<Service>> GetServicesByCategoryAsync(int categoryId)
        {
            var services = await table.Where(s => s.CategoryId == categoryId).ToListAsync();

            return services;
        }

        public async Task<IEnumerable<Service>> GetServicesByTagsAsync(string[] tagNames)
        {
            var services = await table.Where(s => s.Tags.Any(t => tagNames.Contains(t.Name))).ToListAsync();

            return services;
        }
    }
}
