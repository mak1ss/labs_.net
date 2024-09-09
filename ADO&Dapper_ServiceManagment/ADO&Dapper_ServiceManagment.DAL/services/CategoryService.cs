using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;

namespace ADO_Dapper_ServiceManagment.DAL.services
{
    public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;   
        }

        public long CreateCategory(Category entity)
        {
            return unitOfWork.CategoryRepository.Add(entity);
        }

        public void DeleteCategory(Category category)
        {
            unitOfWork.CategoryRepository.Delete(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return unitOfWork.CategoryRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return unitOfWork.CategoryRepository.Get(id);
        }

        public void UpdateCategory(Category entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            unitOfWork.CategoryRepository.Update(entity);    
        }
    }
}
