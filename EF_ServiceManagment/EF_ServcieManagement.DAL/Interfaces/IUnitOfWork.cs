using EF_ServcieManagement.DAL.Interfaces.Repositories;

namespace EF_ServcieManagement.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IServiceRepository ServiceRepository { get; }
        ITagRepository TagRepository { get; }

        Task SaveChangesAsync();
    }
}
