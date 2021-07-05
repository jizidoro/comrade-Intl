#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.UseCases
{
    public class SystemUserCreateUseCase : Service
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISystemUserRepository _repository;

        public SystemUserCreateUseCase(ISystemUserRepository repository,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<SystemUser>> Execute(SystemUser entity)
        {
            try
            {
                var isValid = ValidateEntity(entity);
                if (!isValid.Success)
                {
                    return isValid;
                }

                var validate = SystemUserCreateValidation.Execute(entity);
                if (!validate.Success) return validate;

                entity.Password = _passwordHasher.Hash(entity.Password);
                entity.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();

                await _repository.Add(entity).ConfigureAwait(false);

                _ = await Commit().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return new SingleResult<SystemUser>(BusinessMessage.MSG07);
            }

            return new CreateResult<SystemUser>();
        }
    }
}