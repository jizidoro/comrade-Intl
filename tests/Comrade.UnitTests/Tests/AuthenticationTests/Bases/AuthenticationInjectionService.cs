#region

using AutoMapper;
using Comrade.Application.Services.AuthenticationServices.Commands;
using Comrade.Persistence.DataAccess;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases
{
    public class AuthenticationInjectionService
    {
        private readonly AuthenticationInjectionUseCase _authenticationInjectionUseCase = new();

        public AuthenticationCommand GetAuthenticationCommand(ComradeContext context,
            IMapper mapper)
        {
            var getUpdatePasswordUseCase =
                _authenticationInjectionUseCase.GetUpdatePasswordUseCase(context);
            var getForgotPasswordUseCase =
                _authenticationInjectionUseCase.GetForgotPasswordUseCase(context);
            var getValidateLoginUseCaseUseCase =
                _authenticationInjectionUseCase.GetValidateLoginUseCase(context);

            var authenticationService = new AuthenticationCommand(getUpdatePasswordUseCase,
                getValidateLoginUseCaseUseCase, getForgotPasswordUseCase, mapper);
            return authenticationService;
        }
    }
}