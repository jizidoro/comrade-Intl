#region

using System;
using System.Threading.Tasks;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.UserSystemCore.Validations;
using comrade.Domain.Extensions;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UserSystemCore.UseCases
{
    public class UserSystemCreateUseCase : Service
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserSystemRepository _repository;
        private readonly UserSystemCreateValidation _userSystemCreateValidation;

        public UserSystemCreateUseCase(IUserSystemRepository repository,
            UserSystemCreateValidation userSystemCreateValidation,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _userSystemCreateValidation = userSystemCreateValidation;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<UserSystem>> Execute(UserSystem entity)
        {
            try
            {
                var isValid = ValidateEntidade(entity);
                if (!isValid.Success)
                {
                    return isValid;
                }

                var validate = _userSystemCreateValidation.Execute(entity);
                if (!validate.Success) return validate;

                entity.Password = _passwordHasher.Hash(entity.Password);
                entity.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();

                await _repository.Add(entity);

                var success = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<UserSystem>(BusinessMessage.MSG07);
            }

            return new CreateResult<UserSystem>(entity);
        }
    }
}