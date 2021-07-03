#region

using Comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace Comrade.Application.Validations.AirplaneValidations
{
    public class AirplaneEditValidation : AirplaneValidation<AirplaneEditDto>
    {
        public AirplaneEditValidation()
        {
            ValidateId();
            ValidateCode();
            ValidateModel();
            ValidateQuantidadePassageiro();
        }
    }
}