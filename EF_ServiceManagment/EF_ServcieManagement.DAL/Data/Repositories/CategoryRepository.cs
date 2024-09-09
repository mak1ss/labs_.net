using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Exceptions;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EF_ServcieManagement.DAL.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ServiceManagementContext context) : base(context) { }


        public override async Task<Category> GetCompleteEntityAsync(int id)
        {
            var category = await table.Include(c => c.Services).SingleOrDefaultAsync(c => c.Id == id);

            return category ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }
    }
}
