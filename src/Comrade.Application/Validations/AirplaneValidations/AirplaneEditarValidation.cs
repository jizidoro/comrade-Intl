#region

using Comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace Comrade.Application.Validations.AirplaneValidations
{
    public class AirplaneEditarValidation : AirplaneValidation<AirplaneEditarDto>
    {
        public AirplaneEditarValidation()
        {
            ValidarId();
            ValidarCodigo();
            ValidarModelo();
            ValidarQuantidadePassageiro();
        }
    }
}