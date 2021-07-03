#region

using Comrade.Core.AirplaneCore;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.UsuarioSistemaCore;
using Comrade.Core.Views.VBaUsuPermissaoCore;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.Infrastructure.Repositories.Views;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

#endregion

namespace Comrade.WebApi.Modules
{
    /// <summary>
    ///     Persistence Extensions.
    /// </summary>
    public static class EntityRepositoryExtensions
    {
        /// <summary>
        ///     Add Persistence dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddEntityRepository(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            IFeatureManager featureManager = services
                .BuildServiceProvider()
                .GetRequiredService<IFeatureManager>();

            var isEnabled = featureManager
                .IsEnabledAsync(nameof(CustomFeature.SqlServer))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            if (isEnabled)
            {
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IAirplaneRepository, AirplaneRepository>();
                services.AddScoped<IUsuarioSistemaRepository, UsuarioSistemaRepository>();
                services.AddScoped<IVwUsuarioSistemaPermissaoRepository, VwUsuarioSistemaPermissaoRepository>();
            }

            return services;
        }
    }
}