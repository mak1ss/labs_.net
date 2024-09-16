using AutoMapper;
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using EF_ServiceManagement.BLL.DTO.Category;
using EF_ServiceManagement.BLL.Interfaces.Services;

namespace EF_ServiceManagement.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryRepository repo;

        public CategoryService(IUnitOfWork unit, IMapper mapper) {
            this.mapper = mapper;
            unitOfWork = unit;
            repo = unitOfWork.CategoryRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryResponse>> GetAsync()
        {
            var entities = await repo.GetAsync();
            return entities.Select(mapper.Map<Category, CategoryResponse>);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var entity = await repo.GetByIdAsync(id);
            return mapper.Map<Category, CategoryResponse>(entity);
        }

        public async Task InsertAsync(CategoryRequest request)
        {
            await repo.InsertAsync(mapper.Map<CategoryRequest, Category>(request));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryRequest request)
        {
            await repo.UpdateAsync(mapper.Map<CategoryRequest, Category>(request));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
