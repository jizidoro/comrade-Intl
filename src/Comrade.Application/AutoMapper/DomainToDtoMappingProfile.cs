#region

using AutoMapper;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Domain.Bases;
using comrade.Domain.Models;

#endregion

namespace comrade.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Entity, EntityDto>();
            CreateMap<LookupEntity, LookupDto>();

            CreateMap<Airplane, AirplaneEditDto>();
            CreateMap<Airplane, AirplaneDto>();

            CreateMap<UserSystem, UserSystemEditDto>();
            CreateMap<UserSystem, UserSystemDto>();

            CreateMap<UserSystem, AthenticationDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}