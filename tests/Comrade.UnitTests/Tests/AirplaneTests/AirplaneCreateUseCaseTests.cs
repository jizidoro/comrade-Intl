#region

using System;
using System.Threading.Tasks;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Comrade.UnitTests.Tests.AirplaneTests.TestDatas;
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
                .EnableSensitiveDataLogging().Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();

            var airplaneCreateUseCase = _airplaneInjectionUseCase.GetAirplaneCreateUseCase(context);
            var result = await airplaneCreateUseCase.Execute(testObjectInput);

            Assert.Equal(expected, result.Code);
        }

        [Fact]
        public async Task AirplaneCreateUseCase_Test_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseSqlServer("error")
                .EnableSensitiveDataLogging().Options;

            var testObject = new Airplane
            {
                Id = 1,
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            };

            await using var context = new ComradeContext(options);

            var airplaneCreateUseCase = _airplaneInjectionUseCase.GetAirplaneCreateUseCase(context);
            try
            {
                var result = await airplaneCreateUseCase.Execute(testObject);
                Assert.True(false);
            }
            catch (Exception e)
            {
                Assert.NotEmpty(e.Message);
            }
        }
    }
}