#region

using Comrade.Application.Bases;
using Comrade.Application.Dtos.UsuarioSistemaDtos;
using Comrade.Application.Messages;
using FluentValidation;

#endregion

namespace Comrade.Application.Validations.UsuarioSistemaValidations
{
    public class UsuarioSistemaValidation<TDto> : DtoValidation<TDto>
        where TDto : UsuarioSistemaDto
    {
        protected void ValidarId()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }

        protected void ValidarNome()
        {
            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Nome");
        }

        protected void ValidarEmail()
        {
            RuleFor(v => v.Email)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Email");
        }

        protected void ValidarSenha()
        {
            RuleFor(v => v.Senha)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MinimumLength(4).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .MaximumLength(127).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Senha");
        }


        protected void ValidarMatricula()
        {
            RuleFor(v => v.Matricula)
                .NotEmpty().WithMessage(MensagensAplicacao.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(MensagensAplicacao.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Matricula");
        }
    }
}