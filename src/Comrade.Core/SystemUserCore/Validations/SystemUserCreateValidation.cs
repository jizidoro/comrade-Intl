#region

using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.Validations
{
    public class SystemUserCreateValidation : EntityValidation<SystemUser>
    {
        private readonly ISystemUserRepository _repository;

        public SystemUserCreateValidation(ISystemUserRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ISingleResult<SystemUser> Execute(SystemUser entity)
        {
            return new SingleResult<SystemUser>(entity);
        }
    }
}