#region

using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Messages;
using FluentValidation;

#endregion

namespace comrade.Application.Validations.AirplaneValidations
{
    public class AirplaneValidation<TDto> : DtoValidation<TDto>
        where TDto : AirplaneDto
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }

        protected void ValidateCode()
        {
            RuleFor(v => v.Code)
                .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Code");
        }

        protected void ValidateModel()
        {
            RuleFor(v => v.Model)
                .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Model");
        }

        protected void ValidatePassengerQuantity()
        {
            RuleFor(v => v.PassengerQuantity)
                .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
                .GreaterThanOrEqualTo(0).WithMessage(ApplicationMessage.CAMPO_MAIOR_IGUAL_ZERO)
                .WithName("PassengerQuantity");
        }
    }
}