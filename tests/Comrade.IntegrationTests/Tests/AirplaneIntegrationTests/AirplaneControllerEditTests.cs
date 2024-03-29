﻿#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneControllerEditTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_Edit()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneController_Edit")
                .EnableSensitiveDataLogging().Options;

            var changeCode = "Code testObject edit";
            var changeModel = "Model testObject edit";

            var testObject = new AirplaneEditDto
            {
                Id = 1,
                Code = changeCode,
                Model = changeModel,
                PassengerQuantity = 6666
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            InjectDataOnContextBase.InitializeDbForTests(context);
            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var result = await airplaneController.Edit(testObject);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue?.Code);
            }

            var repository = new AirplaneRepository(context);
            var airplane = await repository.GetById(1);
            Assert.Equal(6666, airplane!.PassengerQuantity);
            Assert.Equal(changeCode, airplane.Code);
            Assert.Equal(changeModel, airplane.Model);
        }

        [Fact]
        public async Task AirplaneController_Edit_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneController_Edit_Error")
                .EnableSensitiveDataLogging().Options;

            var changeCode = "Code testObject edit";
            var changeModel = "Model testObject edit";

            var testObject = new AirplaneEditDto
            {
                Id = 1,
                Code = changeCode,
                PassengerQuantity = 6666
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            InjectDataOnContextBase.InitializeDbForTests(context);
            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var result = await airplaneController.Edit(testObject);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue?.Code);
            }

            var repository = new AirplaneRepository(context);
            var airplane = await repository.GetById(1);
            Assert.NotEqual(6666, airplane!.PassengerQuantity);
            Assert.NotEqual(changeCode, airplane.Code);
            Assert.NotEqual(changeModel, airplane.Model);
        }
    }
}