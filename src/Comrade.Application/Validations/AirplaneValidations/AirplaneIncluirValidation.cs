#region

using Comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace Comrade.Application.Validations.AirplaneValidations
{
    public class AirplaneCreateValidation : AirplaneValidation<AirplaneCreateDto>
    {
        public AirplaneCreateValidation()
        {
            ValidateCode();
            ValidateModel();
            ValidateQuantidadePassageiro();
        }
    }
}