#region

using Comrade.Core.AirplaneCore;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.UserSystemCore;
using Comrade.Core.Views.VBaUsuPermissaoCore;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.Infrastructure.Repositories.Views;
using Microsoft.Extensions.DependencyInjection;

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
            this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAirplaneRepository, AirplaneRepository>();
            services.AddScoped<IUserSystemRepository, UserSystemRepository>();
            services.AddScoped<IVwUserSystemPermissionRepository, VwUserSystemPermissionRepository>();

            return services;
        }
    }
}