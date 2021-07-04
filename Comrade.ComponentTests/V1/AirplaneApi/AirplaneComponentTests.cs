#region

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

#endregion

namespace Comrade.ComponentTests.V1.AirplaneApi
{
    public class AirplaneComponentTests
    {
        private readonly CustomWebApplicationFactoryFixture _fixture;

        public AirplaneComponentTests(CustomWebApplicationFactoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAccountsReturnsList()
        {
            HttpClient client = _fixture
                .CustomWebApplicationFactory
                .CreateClient();

            HttpResponseMessage actualResponse = await client
                .GetAsync("/api/v1/Accounts/")
                .ConfigureAwait(false);

            string actualResponseString = await actualResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

            using StringReader stringReader = new(actualResponseString);
            using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
            JObject jsonResponse = await JObject.LoadAsync(reader)
                .ConfigureAwait(false);

            Assert.Equal(JTokenType.String, jsonResponse["accounts"]![0]!["accountId"]!.Type);
            Assert.Equal(JTokenType.Integer, jsonResponse["accounts"]![0]!["currentBalance"]!.Type);

            Assert.True(Guid.TryParse(jsonResponse["accounts"]![0]!["accountId"]!.Value<string>(), out var _));
            Assert.True(decimal.TryParse(jsonResponse["accounts"]![0]!["currentBalance"]!.Value<string>(),
                out var _));
        }
    }
}