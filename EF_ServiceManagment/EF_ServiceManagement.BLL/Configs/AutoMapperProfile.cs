
using AutoMapper;
using EF_ServcieManagement.DAL.Entities;
using EF_ServcieManagement.DAL.Interfaces.Repositories;
using EF_ServiceManagement.BLL.DTO.Category;
using EF_ServiceManagement.BLL.DTO.Service;
using EF_ServiceManagement.BLL.DTO.Tag;

namespace EF_ServiceManagement.BLL.Configs
{
    public class AutoMapperProfile : Profile
    {
        private readonly ITagRepository tagRepository;
        public AutoMapperProfile(ITagRepository tagRepository) 
        {
            this.tagRepository = tagRepository;
            CreateCategoryMapping();
            CreateTagMapping();
            CreateServiceMapping();
        }

        private void CreateCategoryMapping()
        {
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();
        }

        private void CreateServiceMapping()
        {
            CreateMap<ServiceRequest, Service>()
                .ForMember(entity => entity.Tags,
                options => options.MapFrom(request => tagRepository.GetTagsByIds(request.TagIds)));
            CreateMap<Service, ServiceResponse>();
        }

        private void CreateTagMapping()
        {
            CreateMap<TagRequest, Tag>();
            CreateMap<Tag, TagResponse>();
        }
    }
}
