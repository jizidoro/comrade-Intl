#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.UseCases
{
    public class SystemUserEditUseCase : Service
    {
        private readonly ISystemUserRepository _repository;
        private readonly SystemUserEditValidation _systemUserEditValidation;

        public SystemUserEditUseCase(ISystemUserRepository repository,
            SystemUserEditValidation systemUserEditValidation, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _systemUserEditValidation = systemUserEditValidation;
        }

        public async Task<ISingleResult<SystemUser>> Execute(SystemUser entity)
        {
            try
            {
                var isValid = ValidateEntidade(entity);
                if (!isValid.Success)
                {
                    return isValid;
                }

                var result = await _systemUserEditValidation.Execute(entity);
                if (!result.Success) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                _ = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<SystemUser>(ex);
            }

            return new EditResult<SystemUser>();
        }

        private void HydrateValues(SystemUser target, SystemUser source)
        {
            target.Name = source.Name;
            target.Email = source.Email;
            target.Registration = source.Registration;
            target.Situacao = source.Situacao;
        }
    }
}