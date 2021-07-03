#region

using System;
using Comrade.Application.Interfaces;
using Comrade.Application.Lookups;
using Comrade.Application.Services;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.Bases;
using Comrade.WebApi.Modules;
using Comrade.WebApi.Modules.Common;
using Comrade.WebApi.Modules.Common.FeatureFlags;
using Comrade.WebApi.Modules.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public static class WebHostBuilderExt
    {
        public static WebHostBuilder ConfigureServicesTest(this WebHostBuilder @this,
            Action<IServiceCollection> configureServices)
        {
            @this.ConfigureServices(services =>
                {
                    configureServices(services);

                    services.AddMvc();
                })
                .Configure(builder => { builder.UseMvc(); });
            return @this;
        }
    }
}