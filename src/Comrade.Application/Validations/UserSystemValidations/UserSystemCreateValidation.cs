#region

using comrade.Application.Dtos.UserSystemDtos;

#endregion

namespace comrade.Application.Validations.UserSystemValidations
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