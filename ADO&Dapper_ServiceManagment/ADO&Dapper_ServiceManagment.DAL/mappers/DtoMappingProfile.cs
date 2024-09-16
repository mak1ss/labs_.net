using ADO_Dapper_ServiceManagment.DAL.dtos.category;
using ADO_Dapper_ServiceManagment.DAL.dtos.service;
using ADO_Dapper_ServiceManagment.DAL.dtos.serviceTag;
using ADO_Dapper_ServiceManagment.DAL.dtos.tag;
using ADO_Dapper_ServiceManagment.DAL.entities;
using AutoMapper;

namespace ADO_Dapper_ServiceManagment.DAL.mappers
{
    public class DtoMappingProfile : Profile
    {

        public DtoMappingProfile()
        {
            //service
            CreateMap<ServiceRequest, Service>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Service, ServiceResponse>();

            //category
            CreateMap<CategoryRequest, Category>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Category, CategoryResponse>();

            //serviceTag
            CreateMap<ServiceTagRequest, ServiceTag>();
            CreateMap<ServiceTag, ServiceTagResponse>();

            //tag
            CreateMap<TagRequest, Tag>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Tag, TagResponse>();
        }
    }
}
