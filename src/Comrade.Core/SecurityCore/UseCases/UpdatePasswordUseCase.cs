#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore.UseCases
{
    public class UpdatePasswordUseCase : Service
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISystemUserRepository _repository;
        private readonly SystemUserEditValidation _systemUserEditValidation;

        public UpdatePasswordUseCase(ISystemUserRepository repository,
            SystemUserEditValidation systemUserEditValidation,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _systemUserEditValidation = systemUserEditValidation;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<SystemUser>> Execute(SystemUser entity)
        {
            try
            {
                var result = await _systemUserEditValidation.Execute(entity);
                if (!result.Success) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                var success = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<SystemUser>(ex);
            }

            return new EditResult<SystemUser>();
        }

        private void HydrateValues(SystemUser target, SystemUser source)
        {
            target.Password = _passwordHasher.Hash(source.Password);
        }
    }
}