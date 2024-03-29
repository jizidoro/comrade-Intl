﻿#region

using System.Threading.Tasks;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneControllerDeleteTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_Delete()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneController_Delete")
                .EnableSensitiveDataLogging().Options;


            var idAirplane = 1;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            InjectDataOnContextBase.InitializeDbForTests(context);

            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            _ = await airplaneController.Delete(idAirplane);

            var repository = new AirplaneRepository(context);
            var airplane = await repository.GetById(1);
            Assert.Null(airplane);
        }
    }
}