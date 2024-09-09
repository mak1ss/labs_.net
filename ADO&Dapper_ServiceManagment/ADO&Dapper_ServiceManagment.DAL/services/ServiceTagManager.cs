using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;

namespace ADO_Dapper_ServiceManagment.DAL.services
{
    public class ServiceTagManager : IServiceTagManager
    {
        private readonly IUnitOfWork unitOfWork;

        public ServiceTagManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public long CreateServiceTag(ServiceTag entity)
        {
            return unitOfWork.ServiceTagRepository.Add(entity);
        }

        public void DeleteServiceTag(ServiceTag entity)
        {
            unitOfWork.ServiceTagRepository.Delete(entity);
        }

        public IEnumerable<ServiceTag> GetAllServiceTags()
        {
            return unitOfWork.ServiceTagRepository.GetAll();
        }

        public ServiceTag GetServiceTag(int id)
        {
            return unitOfWork.ServiceTagRepository.Get(id);
        }

        public void UpdateServiceTag(ServiceTag entity)
        {
            unitOfWork.ServiceTagRepository.Update(entity);
        }
    }
}
