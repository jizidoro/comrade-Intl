#region

using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Interfaces;
using Comrade.Application.Services;
using Comrade.Application.Validations.AirplaneValidations;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.SecurityCore;
using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Core.SystemUserCore.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AirplaneCreateValidation = Comrade.Core.AirplaneCore.Validations.AirplaneCreateValidation;
using AirplaneDeleteValidation = Comrade.Core.AirplaneCore.Validations.AirplaneDeleteValidation;
using AirplaneEditValidation = Comrade.Core.AirplaneCore.Validations.AirplaneEditValidation;

#endregion

namespace Comrade.WebApi.Modules
{
    /// <summary>
    ///     Adds Use Cases classes.
    /// </summary>
    public static class UseCasesExtensions
    {
        /// <summary>
        ///     Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            #region Authentication

            // Application - Services
            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();

            // Core - UseCases
            services.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
            services.AddScoped<IValidateLoginUseCase, ValidateLoginUseCase>();
            services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
            services.AddScoped<IGenerateTokenUseCase, GenerateTokenUseCase>();

            #endregion

            #region Airplane

            // Application - Services
            services.AddScoped<IAirplaneAppService, AirplaneAppService>();

            // Core - UseCases
            services.AddScoped<IAirplaneEditUseCase, AirplaneEditUseCase>();
            services.AddScoped<IAirplaneCreateUseCase, AirplaneCreateUseCase>();
            services.AddScoped<IAirplaneDeleteUseCase, AirplaneDeleteUseCase>();

            // Core - Validations
            services.AddScoped<AirplaneEditValidation>();
            services.AddScoped<AirplaneDeleteValidation>();
            services.AddScoped<AirplaneCreateValidation>();
            services.AddScoped<AirplaneValidateSameCode>();

            #endregion

            #region SystemUser

            // Application - Services
            services.AddScoped<ISystemUserAppService, SystemUserAppService>();

            // Core - UseCases
            services.AddScoped<ISystemUserEditUseCase, SystemUserEditUseCase>();
            services.AddScoped<ISystemUserCreateUseCase, SystemUserCreateUseCase>();
            services.AddScoped<ISystemUserDeleteUseCase, SystemUserDeleteUseCase>();

            // Core - Validations
            services.AddScoped<SystemUserForgotPasswordValidation>();
            services.AddScoped<SystemUserPasswordValidation>();
            services.AddScoped<SystemUserEditValidation>();
            services.AddScoped<SystemUserDeleteValidation>();
            services.AddScoped<SystemUserCreateValidation>();

            #endregion

            return services;
        }
    }
}