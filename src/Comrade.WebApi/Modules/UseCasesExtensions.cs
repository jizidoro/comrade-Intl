#region

using comrade.Application.Interfaces;
using comrade.Application.Services;
using comrade.Core.AirplaneCore.UseCases;
using comrade.Core.AirplaneCore.Validations;
using comrade.Core.SecurityCore.UseCases;
using comrade.Core.SecurityCore.Validation;
using comrade.Core.UserSystemCore.UseCases;
using comrade.Core.UserSystemCore.Validations;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace comrade.WebApi.Modules
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
            #region Athentication

            services.AddScoped<IAthenticationAppService, AthenticationAppService>();

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
            services.AddScoped<AirplaneValidateCodeRepetido>();

            #endregion

            #region UserSystem

            // Application - Services
            services.AddScoped<IUserSystemAppService, UserSystemAppService>();

            // Core - UseCases
            services.AddScoped<UpdatePasswordExpiredUseCase>();
            services.AddScoped<GenerateTokenLoginUseCase>();
            services.AddScoped<ForgotPasswordUseCase>();
            services.AddScoped<UserSystemForgotPasswordValidation>();
            services.AddScoped<UserSystemPasswordValidation>();
            services.AddScoped<UserSystemEditUseCase>();
            services.AddScoped<UserSystemCreateUseCase>();
            services.AddScoped<UserSystemDeleteUseCase>();

            // Core - Validations
            services.AddScoped<UserSystemEditValidation>();
            services.AddScoped<UserSystemDeleteValidation>();
            services.AddScoped<UserSystemCreateValidation>();

            #endregion

            return services;
        }
    }
}