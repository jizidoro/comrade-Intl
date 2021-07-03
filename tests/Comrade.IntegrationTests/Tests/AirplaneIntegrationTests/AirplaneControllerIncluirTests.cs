#region

using System.Linq;
using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public sealed class AirplaneControllerIncluirTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_Incluir()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_change_database_AirplaneController_Incluir")
                .Options;


            var teste = new AirplaneIncluirDto
            {
                Codigo = "123",
                Modelo = "234",
                QuantidadePassageiro = 456
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var airplaneController = _airplaneInjectionController.ObterAirplaneController(context);
            var result = await airplaneController.Incluir(teste);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Codigo);
            }

            Assert.Equal(1, context.Airplanes.Count());
        }

        [Fact]
        public async Task AirplaneController_Incluir_Erro()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_change_database_AirplaneController_Incluir_Erro")
                .Options;


            var teste = new AirplaneIncluirDto
            {
                Codigo = "123",
                QuantidadePassageiro = 456
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var airplaneController = _airplaneInjectionController.ObterAirplaneController(context);
            var result = await airplaneController.Incluir(teste);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue.Codigo);
            }

            Assert.Equal(0, context.Airplanes.Count());
        }
    }
}