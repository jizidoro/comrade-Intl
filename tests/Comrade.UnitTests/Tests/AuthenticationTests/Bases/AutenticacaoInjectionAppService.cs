#region

using Comrade.Application.Services;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories.Views;
using Comrade.UnitTests.Helpers;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases
{
    public class AuthenticationInjectionAppService
    {
        private readonly AuthenticationInjectionUseCase _authenticationInjectionUseCase = new();

        public AuthenticationAppService GetAuthenticationAppServiceService(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();

            var getUpdatePasswordUseCase =
                _authenticationInjectionUseCase.GetUpdatePasswordUseCase(context);
            var getForgotPasswordUseCase =
                _authenticationInjectionUseCase.GetForgotPasswordUseCase(context);
            var getGenerateTokenLoginUseCaseUseCase =
                _authenticationInjectionUseCase.GetGenerateTokenLoginUseCase(context);

            var authenticationAppService = new AuthenticationAppService(getUpdatePasswordUseCase,
                getGenerateTokenLoginUseCaseUseCase, getForgotPasswordUseCase, mapper);
            return authenticationAppService;
        }
    }
}