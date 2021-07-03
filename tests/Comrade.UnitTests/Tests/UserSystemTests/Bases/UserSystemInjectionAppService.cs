#region

using AutoMapper;
using comrade.Application.Services;
using comrade.Core.UserSystemCore.UseCases;
using comrade.Core.UserSystemCore.Validations;
using comrade.Domain.Extensions;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;

#endregion

namespace comrade.UnitTests.Tests.UserSystemTests.Bases
{
    public sealed class UserSystemInjectionAppService
    {
        public UserSystemAppService GetUserSystemAppService(ComradeContext context, IMapper mapper)
        {
            var uow = new UnitOfWork(context);
            var userSystemRepository = new UserSystemRepository(context);
            var passwordHasher = new PasswordHasher(new HashingOptions());

            var userSystemEditValidation =
                new UserSystemEditValidation(userSystemRepository);
            var userSystemDeleteValidation = new UserSystemDeleteValidation(userSystemRepository);
            var userSystemCreateValidation =
                new UserSystemCreateValidation(userSystemRepository);
            var userSystemCreateUseCase =
                new UserSystemCreateUseCase(userSystemRepository, userSystemCreateValidation, passwordHasher,
                    uow);
            var userSystemDeleteUseCase =
                new UserSystemDeleteUseCase(userSystemRepository, userSystemDeleteValidation, uow);
            var userSystemEditUseCase =
                new UserSystemEditUseCase(userSystemRepository, userSystemEditValidation, uow);

            return new UserSystemAppService(userSystemRepository, userSystemEditUseCase,
                userSystemCreateUseCase,
                userSystemDeleteUseCase, mapper);
        }
    }
}