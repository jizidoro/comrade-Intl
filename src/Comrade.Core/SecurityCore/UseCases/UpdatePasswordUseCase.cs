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
    public class UpdatePasswordUseCase : Service, IUpdatePasswordUseCase
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
            var result = await _systemUserEditValidation.Execute(entity).ConfigureAwait(false);
            if (!result.Success) return result;

            var obj = result.Data!;

            HydrateValues(obj, entity);

            _repository.Update(obj);

            _ = await Commit().ConfigureAwait(false);


            return new SingleResult<SystemUser>(entity);
        }

        private void HydrateValues(SystemUser target, SystemUser source)
        {
            target.Password = _passwordHasher.Hash(source.Password);
        }
    }
}