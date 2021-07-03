#region

using AutoMapper;
using comrade.Application.Dtos;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Domain.Models;

#endregion

namespace comrade.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<AirplaneCreateDto, Airplane>();
            CreateMap<UserSystemCreateDto, UserSystem>();
            CreateMap<AthenticationDto, UserSystem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}