using System.Collections.Generic;
using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.Utils;
using Microsoft.Extensions.Configuration;

namespace Comrade.ComponentTests
{
    public static class GenerateFakeToken
    {
        public static string Execute()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:Key", "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var generateTokenUseCase = new GenerateTokenUseCase(configuration);

            var roles = new List<string>()
            {
                "Test"
            };

            var user = new TokenUser("1", "Test", "", roles);


            var token = generateTokenUseCase.Execute(user);
            return token;
        }
    }
}