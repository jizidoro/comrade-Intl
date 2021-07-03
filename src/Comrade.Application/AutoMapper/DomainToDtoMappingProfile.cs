#region

using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Dtos;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Dtos.UsuarioSistemaDtos;
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

            CreateMap<Airplane, AirplaneEditarDto>();
            CreateMap<Airplane, AirplaneDto>();

            CreateMap<UsuarioSistema, UsuarioSistemaEditarDto>();
            CreateMap<UsuarioSistema, UsuarioSistemaDto>();

            CreateMap<UsuarioSistema, AutenticacaoDto>()
                .ForMember(dest => dest.Chave, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha));
        }
    }
}