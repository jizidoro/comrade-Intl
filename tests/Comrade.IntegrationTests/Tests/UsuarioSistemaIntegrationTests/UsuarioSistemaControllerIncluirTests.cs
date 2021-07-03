#region

using System.Linq;
using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public sealed class UserSystemControllerCreateTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystemController_Create()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_create_usuario_sistema")
                .Options;


            var teste = new UserSystemCreateDto
            {
                Name = "111",
                Email = "777@teste",
                Password = "123456",
                Situacao = true,
                Registration = "123"
            };


            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            _ = await userSystemController.Create(teste);
            Assert.Equal(1, context.UserSystems.Count());
        }


        [Fact]
        public async Task UserSystemController_Create_Erro()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_create_usuario_sistema_erro")
                .Options;

            var teste = new UserSystemCreateDto
            {
                Email = "777@teste",
                Password = "123456",
                Situacao = true,
                Registration = "123"
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            var result = await userSystemController.Create(teste);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue.Code);
            }

            Assert.False(context.UserSystems.Any());
        }
    }
}