﻿#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Paginations;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneControllerGetAllPaginatedTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_GetAll_Paginated()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneController_GetAll_Paginated")
                .EnableSensitiveDataLogging().Options;


            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            InjectDataOnContextBase.InitializeDbForTests(context);

            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var paginationQuery = new PaginationQuery();
            var result = await airplaneController.GetAll(paginationQuery);

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