#region

using Comrade.Application.Dtos.SystemUserDtos;

#endregion

namespace Comrade.Application.Validations.SystemUserValidations
{
    public class SystemUserEditValidation : SystemUserValidation<SystemUserEditDto>
    {
        public SystemUserEditValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
            PasswordValidation();
            ValidateRegistration();
        }
    }
}