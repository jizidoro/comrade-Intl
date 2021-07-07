#region

using System.Collections.Generic;
using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases
{
    public sealed class AuthenticationInjectionUseCase
    {
        public UpdatePasswordUseCase GetUpdatePasswordUseCase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var systemUserCoreRepository = new SystemUserRepository(context);

            var systemUserCoreEditValidation =
                new SystemUserEditValidation(systemUserCoreRepository
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new UpdatePasswordUseCase(systemUserCoreRepository,
                systemUserCoreEditValidation, passwordHasher, uow);
        }

        public ForgotPasswordUseCase GetForgotPasswordUseCase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var systemUserCoreRepository = new SystemUserRepository(context);
            var systemUserEditValidation = new SystemUserEditValidation(systemUserCoreRepository);
            var systemUserForgotPasswordValidation =
                new SystemUserForgotPasswordValidation(systemUserCoreRepository,
                    systemUserEditValidation
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new ForgotPasswordUseCase(systemUserCoreRepository,
                systemUserForgotPasswordValidation,
                passwordHasher, uow);
        }

        public ValidateLoginUseCase GetValidateLoginUseCase(ComradeContext context)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:Key", "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var systemUserCoreRepository = new SystemUserRepository(context);
            var generateTokenUseCase = new GenerateTokenUseCase(configuration);

            var passwordHasher = new PasswordHasher(new HashingOptions());

            var systemUserPasswordValidation =
                new SystemUserPasswordValidation(systemUserCoreRepository, passwordHasher);

            var generateTokenLoginUseCase =
                new ValidateLoginUseCase
                (
                    systemUserPasswordValidation,
                    generateTokenUseCase
                );
            return generateTokenLoginUseCase;
        }
    }
}