﻿#region

using Comrade.Application.Bases;
using Comrade.Application.Dtos.SystemUserDtos;
using Comrade.Application.Messages;
using FluentValidation;

#endregion

namespace Comrade.Application.Validations.SystemUserValidations
{
    public class SystemUserValidation<TDto> : DtoValidation<TDto>
        where TDto : SystemUserDto
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }

        protected void ValidateName()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Name");
        }

        protected void ValidateEmail()
        {
            RuleFor(v => v.Email)
                .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Email");
        }

        protected void PasswordValidation()
        {
            RuleFor(v => v.Password)
                .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
                .MinimumLength(4).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
                .MaximumLength(127).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Password");
        }


        protected void ValidateRegistration()
        {
            RuleFor(v => v.Registration)
                .NotEmpty().WithMessage(ApplicationMessage.CAMPO_OBRIGATORIO)
                .MaximumLength(255).WithMessage(ApplicationMessage.TAMANHO_ESPECIFICO_CAMPO)
                .WithName("Registration");
        }
    }
}