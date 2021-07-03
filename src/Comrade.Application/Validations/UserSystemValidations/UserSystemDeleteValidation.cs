#region

using Comrade.Application.Dtos.UserSystemDtos;

#endregion

namespace Comrade.Application.Validations.UserSystemValidations
{
    public class UserSystemDeleteValidation : UserSystemValidation<UserSystemDto>
    {
        public UserSystemDeleteValidation()
        {
            ValidateId();
        }
    }
}