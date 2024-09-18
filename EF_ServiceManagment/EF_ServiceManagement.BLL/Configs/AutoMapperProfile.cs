
using AutoMapper;
using EF_ServcieManagement.DAL.Entities;
using EF_ServiceManagement.BLL.DTO.Category;
using EF_ServiceManagement.BLL.DTO.Service;
using EF_ServiceManagement.BLL.DTO.Tag;

namespace EF_ServiceManagement.BLL.Configs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
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
            CreateMap<ServiceRequest, Service>();
            CreateMap<Service, ServiceResponse>();
        }

        private void CreateTagMapping()
        {
            CreateMap<TagRequest, Tag>();
            CreateMap<Tag, TagResponse>();
        }
    }
}
