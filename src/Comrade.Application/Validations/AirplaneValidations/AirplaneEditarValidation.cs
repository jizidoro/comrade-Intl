#region

using comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace comrade.Application.Validations.AirplaneValidations
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