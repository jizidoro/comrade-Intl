﻿#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;

#endregion

namespace Comrade.Application.Services.AirplaneServices.Commands
{
    public interface IAirplaneCommand : IService
    {
        Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto);
        Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto);
        Task<ISingleResultDto<EntityDto>> Delete(int id);
    }
}