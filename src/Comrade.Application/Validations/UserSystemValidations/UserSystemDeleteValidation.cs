#region

using comrade.Application.Dtos.UserSystemDtos;

#endregion

namespace comrade.Application.Validations.UserSystemValidations
{
    public class UserSystemDeleteValidation : UserSystemValidation<UserSystemDto>
    {
        public UserSystemDeleteValidation()
        {
            ValidateId();
        }
    }
}