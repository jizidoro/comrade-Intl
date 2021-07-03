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
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Code");
        }

        protected void ValidateModel()
        {
            RuleFor(v => v.Model)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Model");
        }

        protected void ValidateQuantidadePassageiro()
        {
            RuleFor(v => v.QuantidadePassageiro)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .GreaterThanOrEqualTo(0).WithMessage(MensagensAplicacao.CAMPO_MAIOR_IGUAL_ZERO)
                .WithName("QuantidadePassageiro");
        }
    }
}