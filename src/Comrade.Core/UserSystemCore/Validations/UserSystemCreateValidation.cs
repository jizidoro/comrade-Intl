#region

using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UserSystemCore.Validations
{
    public class UserSystemCreateValidation : EntityValidation<UserSystem>
    {
        private readonly IUserSystemRepository _repository;

        public UserSystemCreateValidation(IUserSystemRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ISingleResult<UserSystem> Execute(UserSystem entity)
        {
            return new SingleResult<UserSystem>(entity);
        }
    }
}