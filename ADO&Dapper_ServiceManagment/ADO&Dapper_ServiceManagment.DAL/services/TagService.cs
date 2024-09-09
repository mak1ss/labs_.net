using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;

namespace ADO_Dapper_ServiceManagment.DAL.services
{
    public class TagService : ITagService
    {

        private readonly IUnitOfWork unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public long CreateTag(Tag entity)
        {
            return unitOfWork.TagRepository.Add(entity);
        }

        public void DeleteTag(Tag entity)
        {
            unitOfWork.TagRepository.Delete(entity);
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return unitOfWork.TagRepository.GetAll();
        }

        public Tag GetTagById(int id)
        {
            return unitOfWork.TagRepository.Get(id);
        }

        public void UpdateTag(Tag entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            unitOfWork.TagRepository.Update(entity);
        }
    }
}
