#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneControllerGetTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_Get()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_return_AirplaneController_Get")
                .Options;


            var idAirplane = 1;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var result = await airplaneController.GetById(idAirplane);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<AirplaneDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Code);
                Assert.NotNull(actualResultValue.Data);
            }
        }
    }
}