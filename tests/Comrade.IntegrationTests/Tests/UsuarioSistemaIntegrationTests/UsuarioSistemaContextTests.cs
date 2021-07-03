#region

using System.Threading.Tasks;
using comrade.Domain.Models;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public class UserSystemContextTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystem_Context()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_obter_usuario_sistema_Respositorio")
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