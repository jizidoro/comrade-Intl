#region

using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UserSystemCore.Validations
{
    public class UserSystemForgotPasswordValidation : EntityValidation<UserSystem>
    {
        private readonly IUserSystemRepository _repository;
        private readonly UserSystemEditValidation _userSystemEditValidation;

        public UserSystemForgotPasswordValidation(IUserSystemRepository repository,
            UserSystemEditValidation userSystemEditValidation)
            : base(repository)
        {
            _repository = repository;
            _userSystemEditValidation = userSystemEditValidation;
        }

        public ISingleResult<UserSystem> Execute(UserSystem entity)
        {
            var registroExiste = _userSystemEditValidation.Execute(entity).Result;

            if (!registroExiste.Success)
            {
                return new SingleResult<UserSystem>(1001, "User não existe");
            }


            return registroExiste;
        }
    }
}