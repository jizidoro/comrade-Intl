#region

using System.Threading.Tasks;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneControllerDeleteTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_Delete()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_return_AirplaneController_Delete")
                .Options;


            var idAirplane = 1;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            _ = await airplaneController.Delete(idAirplane);

            var repository = new AirplaneRepository(context);
            var airplane = await repository.GetById(1);
            Assert.Null(airplane);
        }
    }
}