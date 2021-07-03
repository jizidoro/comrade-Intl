#region

using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UserSystemCore.Validations
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