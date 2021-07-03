#region

using comrade.Application.Bases;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Application.Messages;
using FluentValidation;

#endregion

namespace comrade.Application.Validations.UserSystemValidations
{
    public class UserSystemValidation<TDto> : DtoValidation<TDto>
        where TDto : UserSystemDto
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }

        protected void ValidateName()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Name");
        }

        protected void ValidateEmail()
        {
            RuleFor(v => v.Email)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Email");
        }

        protected void PasswordValidation()
        {
            RuleFor(v => v.Password)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MinimumLength(4).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .MaximumLength(127).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Password");
        }


        protected void ValidateRegistration()
        {
            RuleFor(v => v.Registration)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Registration");
        }
    }
}