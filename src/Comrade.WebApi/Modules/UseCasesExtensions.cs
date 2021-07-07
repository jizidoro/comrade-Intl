#region

using Comrade.Application.Services.AirplaneServices.Commands;
using Comrade.Application.Services.AirplaneServices.Queries;
using Comrade.Application.Services.AuthenticationServices.Commands;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Queries;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.SecurityCore;
using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.SystemUserCore;
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

            // Application - Services
            services.AddScoped<IAuthenticationCommand, AuthenticationCommand>();

            // Core - UseCases
            services.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
            services.AddScoped<IValidateLoginUseCase, ValidateLoginUseCase>();
            services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
            services.AddScoped<IGenerateTokenUseCase, GenerateTokenUseCase>();

            #endregion

            #region Airplane

            // Application - Services
            services.AddScoped<IAirplaneCommand, AirplaneCommand>();
            services.AddScoped<IAirplaneQuery, AirplaneQuery>();

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
            services.AddScoped<ISystemUserCommand, SystemUserCommand>();
            services.AddScoped<ISystemUserQuery, SystemUserQuery>();

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