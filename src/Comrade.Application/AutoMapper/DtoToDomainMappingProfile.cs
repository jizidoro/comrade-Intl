#region

using AutoMapper;
using Comrade.Application.Dtos;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Dtos.UsuarioSistemaDtos;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<AirplaneIncluirDto, Airplane>();
            CreateMap<UsuarioSistemaIncluirDto, UsuarioSistema>();
            CreateMap<AutenticacaoDto, UsuarioSistema>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Chave))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha));
        }
    }
}