using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;

namespace ADO_Dapper_ServiceManagment.DAL.services
{
    public class ServiceManager : IServiceManager
    {

        private readonly IUnitOfWork unitOfWork;

        public ServiceManager(IUnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        }

        public long CreateService(Service entity)
        {
            return unitOfWork.ServiceRepository.Add(entity);
        }

        public void DeleteService(Service entity)
        {
            unitOfWork.ServiceRepository.Delete(entity);
        }

        
        public IEnumerable<Service> GetServicesByTags(params int[] tagIds)
        {
            return unitOfWork.ServiceRepository.GetServicesByTags(tagIds);
        }

        public Service GetServiceById(int id)
        {
            return unitOfWork.ServiceRepository.Get(id);
        }

        public IEnumerable<Service> GetServicesByCategory(int categoryId)
        {
            return unitOfWork.ServiceRepository.GetServicesByCategory(categoryId);
        }

        public void UpdateService(Service entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            unitOfWork.ServiceRepository.Update(entity);
        }

        public IEnumerable<Service> GetAllServices()
        {
            return unitOfWork.ServiceRepository.GetAll();
        }
    }
}
