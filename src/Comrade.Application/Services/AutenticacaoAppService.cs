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
    public class AuthenticationAppService : AppService, IAuthenticationAppService
    {
        private readonly ForgotPasswordUseCase _forgotPasswordUseCase;
        private readonly GenerateTokenLoginUseCase _generateTokenLoginUseCase;
        private readonly UpdatePasswordUseCase _updatePasswordUseCase;

        public AuthenticationAppService(UpdatePasswordUseCase updatePasswordUseCase,
            GenerateTokenLoginUseCase generateTokenLoginUseCase, ForgotPasswordUseCase forgotPasswordUseCase,
            IMapper mapper) :
            base(mapper)
        {
            _updatePasswordUseCase = updatePasswordUseCase;
            _forgotPasswordUseCase = forgotPasswordUseCase;
            _generateTokenLoginUseCase = generateTokenLoginUseCase;
        }

        public async Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto)
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

        public async Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto)
        {
            var evento = Mapper.Map<UserSystem>(dto);

            var result = await _forgotPasswordUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto)
        {
            var evento = Mapper.Map<UserSystem>(dto);

            var result = await _updatePasswordUseCase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}