#region

using Comrade.Application.Dtos.SystemUserDtos;

#endregion

namespace Comrade.Application.Validations.SystemUserValidations
{
    public class SystemUserDeleteValidation : SystemUserValidation<SystemUserDto>
    {
        public SystemUserDeleteValidation()
        {
            ValidateId();
        }
    }
}