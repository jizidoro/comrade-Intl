using System.Threading.Tasks;
using Comrade.UnitTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Comrade.IntegrationTests.Tests
{
    public class WebHostBuilderTests
    {
        [Fact]
        public void WebHostBuilder_Test()
        {
            var host = new WebHostBuilder()
                .ConfigureServicesTest();

            var server = new TestServer(host);

            var client = server.CreateClient();

            var response = client.GetAsync("/health").Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            server.Dispose();

            Assert.NotEmpty(responseString);
            Assert.NotNull(responseString);

        }
    }
}