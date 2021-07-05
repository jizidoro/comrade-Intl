#region

using System;
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
    public sealed class AirplaneEditUseCaseTests

    {
        private readonly AirplaneInjectionUseCase _airplaneInjectionUseCase = new();
        private readonly ITestOutputHelper _output;

        public AirplaneEditUseCaseTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [ClassData(typeof(AirplaneEditTestData))]
        public async Task AirplaneEditUseCase_Test(int expected, Airplane testObjectInput)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneEditUseCase_Test" + testObjectInput.Id)
                .Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var airplaneEditUseCase = _airplaneInjectionUseCase.GetAirplaneEditUseCase(context);
            var result = await airplaneEditUseCase.Execute(testObjectInput);

            Assert.Equal(expected, result.Code);
        }

        [Fact]
        public async Task AirplaneEditUseCase_Test_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseSqlServer("error")
                .Options;

            var testObject = new Airplane
            {
                Id = 1,
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            };

            await using var context = new ComradeContext(options);

            var airplaneEditUseCase = _airplaneInjectionUseCase.GetAirplaneEditUseCase(context);
            try
            {
                var result = await airplaneEditUseCase.Execute(testObject);
                Assert.True(false);
            }
            catch (Exception e)
            {
                Assert.NotEmpty(e.Message);
            }
        }
    }
}