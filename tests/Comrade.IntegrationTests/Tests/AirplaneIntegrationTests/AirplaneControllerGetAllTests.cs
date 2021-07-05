﻿#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneControllerGetAllTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_GetAll()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneController_GetAll")
                .Options;


            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var result = await airplaneController.GetAll(null);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as PageResultDto<AirplaneDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue?.Code);
                Assert.NotNull(actualResultValue?.Data);
                Assert.Equal(3, actualResultValue?.Data?.Count);
            }
        }
    }
}