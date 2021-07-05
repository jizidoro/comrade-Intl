#region

using System.Linq;
using System.Threading.Tasks;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Comrade.UnitTests.Tests.AirplaneTests.TestDatas;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests
{
    public sealed class AirplaneCreateUseCaseTests

    {
        private readonly AirplaneInjectionUseCase _airplaneInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public AirplaneCreateUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [ClassData(typeof(AirplaneCreateTestData))]
        public async Task AirplaneCreateUseCase_Test(int expected, Airplane testObjectInput)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneCreateUseCase_Test")
                .Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();

            var airplaneCreateUseCase = _airplaneInjectionUseCase.GetAirplaneCreateUseCase(context);
            var result = await airplaneCreateUseCase.Execute(testObjectInput);

            Assert.Equal(expected, result.Code);
        }
    }
}