#region

using Comrade.Application.Interfaces;
using Comrade.Application.Services;
using Comrade.Core.AirplaneCore.Usecases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.SecurityCore.Usecase;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.UsuarioSistemaCore.Usecases;
using Comrade.Core.UsuarioSistemaCore.Validations;
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
            #region Autenticacao

            services.AddScoped<IAutenticacaoAppService, AutenticacaoAppService>();

            #endregion

            #region Airplane

            // Application - Services
            services.AddScoped<IAirplaneAppService, AirplaneAppService>();

            // Core - Usecases
            services.AddScoped<AirplaneEditarUsecase>();
            services.AddScoped<AirplaneIncluirUsecase>();
            services.AddScoped<AirplaneExcluirUsecase>();

            // Core - Validations
            services.AddScoped<AirplaneValidarEditar>();
            services.AddScoped<AirplaneValidarExcluir>();
            services.AddScoped<AirplaneValidarIncluir>();
            services.AddScoped<AirplaneValidarCodigoRepetido>();

            #endregion

            #region UsuarioSistema

            // Application - Services
            services.AddScoped<IUsuarioSistemaAppService, UsuarioSistemaAppService>();

            // Core - Usecases
            services.AddScoped<AtualizarSenhaExpiradaUsecase>();
            services.AddScoped<GerarTokenLoginUsecase>();
            services.AddScoped<EsquecerSenhaUsecase>();
            services.AddScoped<UsuarioSistemaValidarEsquecerSenha>();
            services.AddScoped<UsuarioSistemaValidarSenha>();
            services.AddScoped<UsuarioSistemaEditarUsecase>();
            services.AddScoped<UsuarioSistemaIncluirUsecase>();
            services.AddScoped<UsuarioSistemaExcluirUsecase>();

            // Core - Validations
            services.AddScoped<UsuarioSistemaValidarEditar>();
            services.AddScoped<UsuarioSistemaValidarExcluir>();
            services.AddScoped<UsuarioSistemaValidarIncluir>();

            #endregion

            return services;
        }
    }
}