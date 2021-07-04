#region

using Comrade.Application.Interfaces;
using Comrade.Application.Services;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.SecurityCore;
using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Core.SystemUserCore.Validations;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();

            services.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
            services.AddScoped<IValidateLoginUseCase, ValidateLoginUseCase>();
            services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
            services.AddScoped<IGenerateTokenUseCase, GenerateTokenUseCase>();

            #endregion

            #region Airplane

            // Application - Services
            services.AddScoped<IAirplaneAppService, AirplaneAppService>();

            // Core - UseCases
            services.AddScoped<AirplaneEditUseCase>();
            services.AddScoped<AirplaneCreateUseCase>();
            services.AddScoped<AirplaneDeleteUseCase>();

            // Core - Validations
            services.AddScoped<AirplaneEditValidation>();
            services.AddScoped<AirplaneDeleteValidation>();
            services.AddScoped<AirplaneCreateValidation>();
            services.AddScoped<AirplaneValidateCodeRepeated>();

            #endregion

            #region SystemUser

            // Application - Services
            services.AddScoped<ISystemUserAppService, SystemUserAppService>();

            // Core - UseCases

            services.AddScoped<SystemUserForgotPasswordValidation>();
            services.AddScoped<SystemUserPasswordValidation>();
            services.AddScoped<SystemUserEditUseCase>();
            services.AddScoped<SystemUserCreateUseCase>();
            services.AddScoped<SystemUserDeleteUseCase>();

            // Core - Validations
            services.AddScoped<SystemUserEditValidation>();
            services.AddScoped<SystemUserDeleteValidation>();
            services.AddScoped<SystemUserCreateValidation>();

            #endregion

            return services;
        }
    }
}