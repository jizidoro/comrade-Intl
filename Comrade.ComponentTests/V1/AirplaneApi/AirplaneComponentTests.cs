﻿#region

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

#endregion

namespace Comrade.ComponentTests.V1.AirplaneApi
{
    [Collection("WebApi Collection")]
    public class AirplaneComponentTests
    {
        private readonly CustomWebApplicationFactoryFixture _fixture;
        public AirplaneComponentTests(CustomWebApplicationFactoryFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task GetAccountsReturnsList()
        {
            HttpClient client = _fixture
                .CustomWebApplicationFactory
                .CreateClient();

            HttpResponseMessage actualResponse = await client
                .GetAsync("/api/v1/airplane/get-all")
                .ConfigureAwait(false);

            string actualResponseString = await actualResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

            using StringReader stringReader = new(actualResponseString);
            using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
            JObject jsonResponse = await JObject.LoadAsync(reader)
                .ConfigureAwait(false);

            Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["model"]!.Type);
            Assert.Equal(JTokenType.Integer, jsonResponse["data"]![0]!["passengerQuantity"]!.Type);
            
            Assert.True(decimal.TryParse(jsonResponse["data"]![0]!["passengerQuantity"]!.Value<string>(),
                out var _));
        }
    }
}

