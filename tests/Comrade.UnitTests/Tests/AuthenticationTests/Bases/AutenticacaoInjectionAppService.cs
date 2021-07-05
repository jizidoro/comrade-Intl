#region

using AutoMapper;
using Comrade.Application.Services;
using Comrade.Infrastructure.DataAccess;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases
{
    public class AuthenticationInjectionAppService
    {
        private readonly AuthenticationInjectionUseCase _authenticationInjectionUseCase = new();

        public AuthenticationAppService GetAuthenticationAppService(ComradeContext context, IMapper mapper)
        {
            var getUpdatePasswordUseCase =
                _authenticationInjectionUseCase.GetUpdatePasswordUseCase(context);
            var getForgotPasswordUseCase =
                _authenticationInjectionUseCase.GetForgotPasswordUseCase(context);
            var getValidateLoginUseCaseUseCase =
                _authenticationInjectionUseCase.GetValidateLoginUseCase(context);

            var authenticationAppService = new AuthenticationAppService(getUpdatePasswordUseCase,
                getValidateLoginUseCaseUseCase, getForgotPasswordUseCase, mapper);
            return authenticationAppService;
        }
    }
}