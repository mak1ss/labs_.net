using AutoMapper;
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using EF_ServcieManagement.DAL.Interfaces;
using EF_ServiceManagement.BLL.DTO.Tag;
using EF_ServiceManagement.BLL.Interfaces.Services;

namespace EF_ServiceManagement.BLL.Services
{
    public class TagService : ITagService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITagRepository repo;

        public TagService(IUnitOfWork unit, IMapper mapper)
        {
            this.mapper = mapper;
            unitOfWork = unit;
            repo = unitOfWork.TagRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagResponse>> GetAsync()
        {
            var entities = await repo.GetAsync();
            return entities.Select(mapper.Map<Tag, TagResponse>);
        }

        public async Task<TagResponse> GetByIdAsync(int id)
        {
            var entity = await repo.GetByIdAsync(id);
            return mapper.Map<Tag, TagResponse>(entity);
        }

        public async Task InsertAsync(TagRequest request)
        {
            await repo.InsertAsync(mapper.Map<TagRequest, Tag>(request));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagRequest request)
        {
            await repo.UpdateAsync(mapper.Map<TagRequest, Tag>(request));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
