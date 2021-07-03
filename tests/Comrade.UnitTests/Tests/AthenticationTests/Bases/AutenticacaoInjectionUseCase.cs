#region

using System.Collections.Generic;
using comrade.Application.Services;
using comrade.Core.SecurityCore.UseCases;
using comrade.Core.SecurityCore.Validation;
using comrade.Core.UserSystemCore.Validations;
using comrade.Domain.Extensions;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.Infrastructure.Repositories.Views;
using comrade.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;

#endregion

namespace comrade.UnitTests.Tests.AthenticationTests.Bases
{
    public sealed class AthenticationInjectionUseCase
    {
        public UpdatePasswordExpiredUseCase GetUpdatePasswordExpiredUseCase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var userSystemCoreRepository = new UserSystemRepository(context);

            var userSystemCoreEditValidation =
                new UserSystemEditValidation(userSystemCoreRepository
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new UpdatePasswordExpiredUseCase(userSystemCoreRepository,
                userSystemCoreEditValidation, passwordHasher, uow);
        }

        public ForgotPasswordUseCase GetForgotPasswordUseCase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var userSystemCoreRepository = new UserSystemRepository(context);
            var userSystemEditValidation = new UserSystemEditValidation(userSystemCoreRepository);
            var userSystemForgotPasswordValidation =
                new UserSystemForgotPasswordValidation(userSystemCoreRepository, userSystemEditValidation
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new ForgotPasswordUseCase(userSystemCoreRepository, userSystemForgotPasswordValidation,
                passwordHasher, uow);
        }

        public GenerateTokenLoginUseCase GetGenerateTokenLoginUseCase(ComradeContext context)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:Key", "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var uow = new UnitOfWork(context);
            var userSystemCoreRepository = new UserSystemRepository(context);

            var passwordHasher = new PasswordHasher(new HashingOptions());

            var userSystemPasswordValidation =
                new UserSystemPasswordValidation(userSystemCoreRepository, passwordHasher);
            var vUserSystemPermissionRepository =
                new VwUserSystemPermissionRepository(context);
            var generateTokenLoginUseCase =
                new GenerateTokenLoginUseCase
                (
                    configuration,
                    userSystemPasswordValidation
                );
            return generateTokenLoginUseCase;
        }

        private AthenticationAppService GetUserSystemAppService(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var vUserSystemRepository = new VwUserSystemPermissionRepository(context);
            var mapper = MapperHelper.ConfigMapper();

            var oterUpdatePasswordExpiredUseCase = GetUpdatePasswordExpiredUseCase(context);
            var obterForgotPasswordUseCase = GetForgotPasswordUseCase(context);
            var obterGenerateTokenLoginUseCaseUseCase = GetGenerateTokenLoginUseCase(context);

            var authenticationAppService = new AthenticationAppService(vUserSystemRepository,
                oterUpdatePasswordExpiredUseCase,
                obterGenerateTokenLoginUseCaseUseCase, obterForgotPasswordUseCase, mapper);
            return authenticationAppService;
        }
    }
}