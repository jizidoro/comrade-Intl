#region

using comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace comrade.Application.Validations.AirplaneValidations
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