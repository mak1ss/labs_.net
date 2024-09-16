using AutoMapper;
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using EF_ServcieManagement.DAL.Interfaces;
using EF_ServiceManagement.BLL.Interfaces.Services;
using EF_ServiceManagement.BLL.DTO.Service;

namespace EF_ServiceManagement.BLL.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceRepository repo;

        public ServiceManager(IUnitOfWork unit, IMapper mapper)
        {
            this.mapper = mapper;
            unitOfWork = unit;
            repo = unitOfWork.ServiceRepository;
        }

        public async Task<ServiceResponse> AddTagAsync(int tagId, int serviceId)
        {
            var service = await repo.GetByIdAsync(serviceId);
            var tag = await unitOfWork.TagRepository.GetByIdAsync(tagId);

            service.Tags.Add(tag);

            await repo.UpdateAsync(service);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Service, ServiceResponse>(service);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ServiceResponse> DeleteTagAsync(int tagId, int serviceId)
        {
            var service = await repo.GetByIdAsync(serviceId);
            var tag = await unitOfWork.TagRepository.GetByIdAsync(tagId);

            service.Tags.Remove(tag);

            await repo.UpdateAsync(service);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Service, ServiceResponse>(service);
        }

        public async Task<IEnumerable<ServiceResponse>> GetAsync()
        {
            var entities = await repo.GetAsync();
            return entities.Select(mapper.Map<Service, ServiceResponse>);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await repo.GetByIdAsync(id);
            return mapper.Map<Service, ServiceResponse>(entity);
        }

        public async Task<IEnumerable<ServiceResponse>> GetServicesByCategoryAsync(int categoryId)
        {
            var services = await repo.GetServicesByCategoryAsync(categoryId);
            return services.Select(mapper.Map<Service, ServiceResponse>);
        }

        public async Task<IEnumerable<ServiceResponse>> GetServicesByTagsAsync(string[] tagNames)
        {
            var services = await repo.GetServicesByTagsAsync(tagNames);
            return services.Select(mapper.Map<Service, ServiceResponse>);
        }

        public async Task InsertAsync(ServiceRequest request)
        {
            await repo.InsertAsync(mapper.Map<ServiceRequest, Service>(request));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceRequest request)
        {
            await repo.UpdateAsync(mapper.Map<ServiceRequest, Service>(request));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
