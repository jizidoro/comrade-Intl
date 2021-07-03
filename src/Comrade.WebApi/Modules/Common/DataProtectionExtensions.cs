#region

using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.WebApi.Modules.Common
{
    /// <summary>
    ///     Data Protection Extensions.
    /// </summary>
    public static class DataProtectionExtensions
    {
        /// <summary>
        ///     Add Data Protection.
        /// </summary>
        public static IServiceCollection AddCustomDataProtection(this IServiceCollection services)
        {
            return services;
        }
    }
}