#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.UserSystemCore;
using Comrade.Core.UserSystemCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore.UseCases
{
    public class ForgotPasswordUseCase : Service
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserSystemRepository _repository;
        private readonly UserSystemForgotPasswordValidation _userSystemForgotPasswordValidation;

        public ForgotPasswordUseCase(IUserSystemRepository repository,
            UserSystemForgotPasswordValidation userSystemForgotPasswordValidation,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _userSystemForgotPasswordValidation = userSystemForgotPasswordValidation;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<UserSystem>> Execute(UserSystem entity)
        {
            try
            {
                var result = _userSystemForgotPasswordValidation.Execute(entity);
                if (!result.Success) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                _ = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<UserSystem>(ex);
            }

            return new EditResult<UserSystem>();
        }

        private void HydrateValues(UserSystem target, UserSystem source)
        {
            var regraForgotPassword = "123456";
            target.Password = _passwordHasher.Hash(regraForgotPassword);
        }
    }
}