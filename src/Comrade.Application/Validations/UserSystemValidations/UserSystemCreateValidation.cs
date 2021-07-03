#region

using Comrade.Application.Dtos.UserSystemDtos;

#endregion

namespace Comrade.Application.Validations.UserSystemValidations
{
    public class UserSystemCreateValidation : UserSystemValidation<UserSystemCreateDto>
    {
        public UserSystemCreateValidation()
        {
            ValidateName();
            ValidateEmail();
            PasswordValidation();
            ValidateRegistration();
        }
    }
}