#region

using System.Threading.Tasks;
using AutoMapper;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Interfaces;
using comrade.Application.Utils;
using comrade.Core.SecurityCore.UseCases;
using comrade.Core.Views.VBaUsuPermissaoCore;
using comrade.Domain.Models;

#endregion

namespace comrade.Application.Services
{
    public class AthenticationAppService : AppService, IAthenticationAppService
    {
        private readonly UpdatePasswordExpiredUseCase _updatePasswordExpiredUseCase;
        private readonly ForgotPasswordUseCase _esquecerPasswordUseCase;
        private readonly GenerateTokenLoginUseCase _generateTokenLoginUseCase;
        private readonly IVwUserSystemPermissionRepository _vUserSystemPermissionRepository;

        public AthenticationAppService(IVwUserSystemPermissionRepository vUserSystemPermissionRepository,
            UpdatePasswordExpiredUseCase updatePasswordExpiredUseCase,
            GenerateTokenLoginUseCase generateTokenLoginUseCase, ForgotPasswordUseCase esquecerPasswordUseCase, IMapper mapper) :
            base(mapper)
        {
            _vUserSystemPermissionRepository = vUserSystemPermissionRepository;
            _updatePasswordExpiredUseCase = updatePasswordExpiredUseCase;
            _esquecerPasswordUseCase = esquecerPasswordUseCase;
            _generateTokenLoginUseCase = generateTokenLoginUseCase;
        }

        public async Task<ISingleResultDto<UserDto>> GenerateTokenLoginUseCase(AthenticationDto dto)
        {
            var result = await _generateTokenLoginUseCase.Execute(dto.Key, dto.Password);

            if (result.Success)
            {
                var token = new UserDto
                {
                    Token = result.User.Token
                };

                return new SingleResultDto<UserDto>(token);
            }

            return new SingleResultDto<UserDto>(result);
        }

        public async Task<ISingleResultDto<EntityDto>> ForgotPassword(AthenticationDto dto)
        {
            var evento = Mapper.Map<UserSystem>(dto);

            var result = await _esquecerPasswordUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> ExpirarPassword(AthenticationDto dto)
        {
            var evento = Mapper.Map<UserSystem>(dto);

            var result = await _updatePasswordExpiredUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}