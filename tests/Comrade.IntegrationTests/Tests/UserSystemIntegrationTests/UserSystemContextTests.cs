#region

using System.Threading.Tasks;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.UserSystemTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.UserSystemIntegrationTests
{
    public class UserSystemContextTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystem_Context()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UserSystem_Context")
                .Options;

            UserSystem userSystem = null;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);
            var repository = new UserSystemRepository(context);
            userSystem = await repository.GetById(1);
            Assert.NotNull(userSystem);
        }
    }
}