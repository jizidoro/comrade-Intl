#region

using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UserSystemCore.Validations
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