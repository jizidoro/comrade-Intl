#region

using System.Threading.Tasks;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public class UserSystemControllerDeleteTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystemController_Delete()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_Delete_usuario_sistema")
                .Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            _ = await userSystemController.Delete(1);

            var respository = new UserSystemRepository(context);
            var usuario = await respository.GetById(1);
            Assert.Null(usuario);
        }
    }
}