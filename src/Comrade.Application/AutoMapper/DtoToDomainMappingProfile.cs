﻿#region

using AutoMapper;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<AirplaneCreateDto, Airplane>();
            CreateMap<SystemUserCreateDto, SystemUser>();
            CreateMap<AuthenticationDto, SystemUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}