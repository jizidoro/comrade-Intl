#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Models.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore.UseCases
{
    public class ForgotPasswordUseCase : Service, IForgotPasswordUseCase
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISystemUserRepository _repository;
        private readonly SystemUserForgotPasswordValidation _systemUserForgotPasswordValidation;

        public ForgotPasswordUseCase(ISystemUserRepository repository,
            SystemUserForgotPasswordValidation systemUserForgotPasswordValidation,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _systemUserForgotPasswordValidation = systemUserForgotPasswordValidation;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<SystemUser>> Execute(SystemUser entity)
        {
            var result = _systemUserForgotPasswordValidation.Execute(entity);
            if (!result.Success) return result;

            var obj = result.Data!;

            HydrateValues(obj);

            _repository.Update(obj);

            _ = await Commit().ConfigureAwait(false);

            return new EditResult<SystemUser>();
        }

        private void HydrateValues(SystemUser target)
        {
            var ruleForgotPassword = "123456";
            target.Password = _passwordHasher.Hash(ruleForgotPassword);
        }
    }
}