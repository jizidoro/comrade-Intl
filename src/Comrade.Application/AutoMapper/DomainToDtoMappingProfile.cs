#region

using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Dtos;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Dtos.UserSystemDtos;
using Comrade.Application.Lookups;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.AutoMapper
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

            CreateMap<UserSystem, AuthenticationDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}