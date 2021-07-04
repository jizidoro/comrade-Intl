#region

using Comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.WebApi.Modules
{
    /// <summary>
    ///     Persistence Extensions.
    /// </summary>
    public static class SqlServerExtensionsFake
    {
        /// <summary>
        ///     Add Persistence dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddSqlServerFake(
            this IServiceCollection services)
        {
            services.AddDbContext<ComradeContext>(options => options.UseInMemoryDatabase("test_database"));

            return services;
        }
    }
}