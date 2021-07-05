#region

using System.Threading.Tasks;
using AutoMapper;
using Comrade.Application.BaseInterfaces;
using Comrade.Application.Bases;
using Comrade.Application.Dtos;
using Comrade.Application.Interfaces;
using Comrade.Core.SecurityCore;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.Services
{
    public class AuthenticationAppService : AppService, IAuthenticationAppService
    {
        private readonly IForgotPasswordUseCase _forgotPasswordUseCase;
        private readonly IUpdatePasswordUseCase _updatePasswordUseCase;
        private readonly IValidateLoginUseCase _validateLoginUseCase;

        public AuthenticationAppService(IUpdatePasswordUseCase updatePasswordUseCase,
            IValidateLoginUseCase validateLoginUseCase, IForgotPasswordUseCase forgotPasswordUseCase,
            IMapper mapper) :
            base(mapper)
        {
            _updatePasswordUseCase = updatePasswordUseCase;
            _forgotPasswordUseCase = forgotPasswordUseCase;
            _validateLoginUseCase = validateLoginUseCase;
        }

        public async Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto)
        {
            var result = await _validateLoginUseCase.Execute(dto.Key, dto.Password).ConfigureAwait(false);

            if (!result.Success)
            {
                return new SingleResultDto<UserDto>(result);
            }

            var token = new UserDto
            {
                Token = result.TokenUser!.Token
            };

            return new SingleResultDto<UserDto>(token);
        }

        public async Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto)
        {
            var mappedObject = Mapper.Map<SystemUser>(dto);

            var result = await _forgotPasswordUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto)
        {
            var mappedObject = Mapper.Map<SystemUser>(dto);

            var result = await _updatePasswordUseCase.Execute(mappedObject).ConfigureAwait(false);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}