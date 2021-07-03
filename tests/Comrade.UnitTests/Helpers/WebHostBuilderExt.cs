#region

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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