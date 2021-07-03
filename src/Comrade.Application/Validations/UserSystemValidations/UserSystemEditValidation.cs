#region

using Comrade.Application.Dtos.UserSystemDtos;

#endregion

namespace Comrade.Application.Validations.UserSystemValidations
{
    public class UserSystemEditValidation : UserSystemValidation<UserSystemEditDto>
    {
        public UserSystemEditValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
            PasswordValidation();
            ValidateRegistration();
        }
    }
}