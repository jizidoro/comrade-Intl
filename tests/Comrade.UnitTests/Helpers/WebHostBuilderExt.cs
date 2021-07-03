#region

using System;
using Comrade.Application.Lookups;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.Bases;
using Comrade.Infrastructure.DataAccess;
using Comrade.WebApi.Modules;
using Comrade.WebApi.Modules.Common;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Comrade.WebApi.Modules.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Serilog;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public static class WebHostBuilderExt
    {
        public static WebHostBuilder ConfigureServicesTest(this WebHostBuilder @this)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            @this.ConfigureServices(services =>
                {
                    services
                        .AddFeatureFlags(configuration)
                        .AddInvalidRequestLogging()
                        .AddSqlServerFake(configuration)
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

                    services.AddScoped(typeof(ILookupServiceApp<>), typeof(LookupServiceApp<>));
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

                    services.AddScoped<IPasswordHasher, PasswordHasher>();
                    services.AddScoped<HashingOptions>();
                })
                .Configure(builder =>
                {
                    builder
                        .UseProxy(configuration)
                        .UseHealthChecks()
                        .UseCustomCors()
                        .UseCustomHttpMetrics()
                        .UseRouting()
                        // .UseVersionedSwagger(provider, configuration)
                        .UseAuthentication()
                        .UseAuthorization()
                        .UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                            endpoints.MapMetrics();
                        });
                });
            return @this;
        }
    }
}