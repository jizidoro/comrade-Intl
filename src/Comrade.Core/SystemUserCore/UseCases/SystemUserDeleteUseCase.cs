#region

using System.Threading.Tasks;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.UseCases
{
    public class SystemUserDeleteUseCase : CoreService, ISystemUserDeleteUseCase
    {
        private readonly ISystemUserRepository _repository;
        private readonly SystemUserDeleteValidation _systemUserDeleteValidation;

        public SystemUserDeleteUseCase(ISystemUserRepository repository,
            SystemUserDeleteValidation systemUserDeleteValidation,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _systemUserDeleteValidation = systemUserDeleteValidation;
        }

        public async Task<ISingleResult<SystemUser>> Execute(int id)
        {
            var validate = await _systemUserDeleteValidation.Execute(id).ConfigureAwait(false);
            if (!validate.Success) return validate;

            _repository.Remove(id);

            _ = await Commit().ConfigureAwait(false);

            return new DeleteResult<SystemUser>();
        }
    }
}