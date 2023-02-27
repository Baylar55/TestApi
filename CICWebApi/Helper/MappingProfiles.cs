using AutoMapper;
using CICWebApi.DTOs;
using CICWebApi.Entities;
using Data.Entities;

namespace CICWebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Request, RequestDTO>()
                     .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                     .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Type))
                     .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => src.RequestType.Name))
                     .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => src.RequestStatus.Name)).ReverseMap();
            CreateMap<RequestDTO, Request>()
                     .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { Name = src.Category }))
                     .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => new Priority { Type = src.Priority }))
                     .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => new RequestType { Name = src.RequestType }))
                     .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => new RequestStatus { Name = src.RequestStatus }))
                     .ForMember(dest => dest.CreatorUser, opt => opt.MapFrom(src => new User { Name = src.CreatorUser }))
                     .ForMember(dest => dest.ExecutorUser, opt => opt.MapFrom(src => new User { Name = src.ExecutorUser}));
            
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();
            
            CreateMap<PriorityDTO, Priority>();
            CreateMap<Priority, PriorityDTO>();
            
            CreateMap<RequestStatusDTO, RequestStatus>();
            CreateMap<RequestStatus, RequestStatusDTO>();
            
            CreateMap<RequestTypeDTO, RequestType>();
            CreateMap<RequestType, RequestTypeDTO>();

            CreateMap<User, UserDTO>()
                     .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name)).ReverseMap();
            CreateMap<UserDTO, User>();
        }
    }
}
