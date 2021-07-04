#region

using comrade.Application.Dtos.UserSystemDtos;

#endregion

namespace comrade.Application.Validations.UserSystemValidations
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