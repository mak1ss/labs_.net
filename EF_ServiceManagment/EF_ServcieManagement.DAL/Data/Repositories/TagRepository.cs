
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EF_ServcieManagement.DAL.Data.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(ServiceManagementContext context) : base(context) { }

        public override async Task<Tag> GetCompleteEntityAsync(int id)
        {
            var tag = await table.Include(t => t.Services)
                                 .SingleOrDefaultAsync(t => t.Id == id);

            return tag;
        }

        public async Task<IEnumerable<Tag>> GetTagsByIds(int[] ids)
        {
            return await table.Where(t => ids.Contains(t.Id)).ToListAsync();
        }
    }
}
