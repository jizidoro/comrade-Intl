#region

using Comrade.Application.Interfaces;
using Comrade.Application.Services;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Infrastructure.Bases;
using Comrade.WebApi.Modules;
using Comrade.WebApi.Modules.Common;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Comrade.WebApi.Modules.Common.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public class ObterServiceProviderDb
    {
        public ServiceProvider Execute()
        {
            var services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            services
                .AddFeatureFlags(configuration)
                .AddInvalidRequestLogging()
                .AddSqlServer(configuration)
                .AddEntityRepository(configuration)
                .AddHealthChecks(configuration)
                .AddAuthentication(configuration)
                .AddVersioning()
                .AddSwagger()
                .AddUseCases()
                .AddCustomControllers()
                .AddCustomCors()
                .AddProxy()
                .AddCustomDataProtection();

            services.AddAutoMapperSetup();

            services.AddLogging();

            services.AddScoped(typeof(ILookupServiceApp<>), typeof(LookupServiceApp<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            return services.BuildServiceProvider();
        }
    }
}