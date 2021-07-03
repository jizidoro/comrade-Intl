#region

using comrade.Application.Services;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories.Views;
using comrade.UnitTests.Helpers;

#endregion

namespace comrade.UnitTests.Tests.AthenticationTests.Bases
{
    public class AthenticationInjectionAppService
    {
        private readonly AthenticationInjectionUseCase _authenticationInjectionUseCase = new();

        public AthenticationAppService GetAthenticationAppServiceService(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var vUserSystemRepository = new VwUserSystemPermissionRepository(context);
            var mapper = MapperHelper.ConfigMapper();

            var oterUpdatePasswordExpiredUseCase =
                _authenticationInjectionUseCase.GetUpdatePasswordExpiredUseCase(context);
            var obterForgotPasswordUseCase =
                _authenticationInjectionUseCase.GetForgotPasswordUseCase(context);
            var obterGenerateTokenLoginUseCaseUseCase =
                _authenticationInjectionUseCase.GetGenerateTokenLoginUseCase(context);

            var authenticationAppService = new AthenticationAppService(vUserSystemRepository,
                oterUpdatePasswordExpiredUseCase,
                obterGenerateTokenLoginUseCaseUseCase, obterForgotPasswordUseCase, mapper);
            return authenticationAppService;
        }
    }
}