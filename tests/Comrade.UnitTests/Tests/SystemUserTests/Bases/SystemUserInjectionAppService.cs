#region

using AutoMapper;
using Comrade.Application.Services;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;

#endregion

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases
{
    public sealed class SystemUserInjectionAppService
    {
        public SystemUserAppService GetSystemUserAppService(ComradeContext context, IMapper mapper)
        {
            var uow = new UnitOfWork(context);
            var systemUserRepository = new SystemUserRepository(context);
            var passwordHasher = new PasswordHasher(new HashingOptions());

            var systemUserEditValidation =
                new SystemUserEditValidation(systemUserRepository);
            var systemUserDeleteValidation = new SystemUserDeleteValidation(systemUserRepository);
            var systemUserCreateValidation =
                new SystemUserCreateValidation(systemUserRepository);
            var systemUserCreateUseCase =
                new SystemUserCreateUseCase(systemUserRepository, systemUserCreateValidation, passwordHasher,
                    uow);
            var systemUserDeleteUseCase =
                new SystemUserDeleteUseCase(systemUserRepository, systemUserDeleteValidation, uow);
            var systemUserEditUseCase =
                new SystemUserEditUseCase(systemUserRepository, systemUserEditValidation, uow);

            return new SystemUserAppService(systemUserRepository, systemUserEditUseCase,
                systemUserCreateUseCase,
                systemUserDeleteUseCase, mapper);
        }
    }
}