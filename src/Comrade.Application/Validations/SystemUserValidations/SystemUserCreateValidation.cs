#region

using Comrade.Application.Dtos.SystemUserDtos;

#endregion

namespace Comrade.Application.Validations.SystemUserValidations
{
    public class SystemUserCreateValidation : SystemUserValidation<SystemUserCreateDto>
    {
        public SystemUserCreateValidation()
        {
            ValidateName();
            ValidateEmail();
            PasswordValidation();
            ValidateRegistration();
        }
    }
}