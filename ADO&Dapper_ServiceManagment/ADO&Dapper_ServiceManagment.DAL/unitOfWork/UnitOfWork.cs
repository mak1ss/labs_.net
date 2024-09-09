using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.repositories;

namespace ADO_Dapper_ServiceManagment.DAL.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceTagRepository serviceTagRepository;
        private readonly ITagRepository tagRepository;

        public UnitOfWork(ICategoryRepository categoryRepository, IServiceRepository serviceRepository, IServiceTagRepository serviceTagRepository, ITagRepository tagRepository)
        {
            this.categoryRepository = categoryRepository;
            this.serviceRepository = serviceRepository;
            this.serviceTagRepository = serviceTagRepository;
            this.tagRepository = tagRepository;
        }

        public ICategoryRepository CategoryRepository
        {
            get { return categoryRepository; }
        }

        public IServiceRepository ServiceRepository
        {
            get { return serviceRepository; }
        }

        public IServiceTagRepository ServiceTagRepository
        {
            get
            {
                return serviceTagRepository;    
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                return tagRepository;
            }
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
