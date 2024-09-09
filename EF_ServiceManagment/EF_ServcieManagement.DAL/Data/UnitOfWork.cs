

using EF_ServcieManagement.DAL.Interfaces;
using EF_ServcieManagement.DAL.Interfaces.Repositories;

namespace EF_ServcieManagement.DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ServiceManagementContext context;

        public UnitOfWork(ServiceManagementContext context, ICategoryRepository categoryRepository, 
            IServiceRepository serviceRepository, ITagRepository tagRepository)
        {
            this.context = context;
            CategoryRepository = categoryRepository;
            ServiceRepository = serviceRepository;
            TagRepository = tagRepository;
        }

        public ICategoryRepository CategoryRepository { get; }
        public IServiceRepository ServiceRepository { get; }
        public ITagRepository TagRepository { get; }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
