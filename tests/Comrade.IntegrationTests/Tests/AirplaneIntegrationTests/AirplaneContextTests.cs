#region

using System.Threading.Tasks;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public class AirplaneContextTests
    {
        [Fact]
        public async Task Airplane_Context()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_return_Get_ReturnsAirplane")
                .Options;

            Airplane airplane = null;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);
            var repository = new AirplaneRepository(context);
            airplane = await repository.GetById(1);
            Assert.NotNull(airplane);
        }
    }
}