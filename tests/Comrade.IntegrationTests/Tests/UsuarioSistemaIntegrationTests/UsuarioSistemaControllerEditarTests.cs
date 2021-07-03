#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Infrastructure.DataAccess;
using comrade.Infrastructure.Repositories;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UsuarioSistemaIntegrationTests
{
    public class UserSystemControllerEditTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystemController_Edit()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_edit_usuario_sistema")
                .Options;

            var alteracaoName = "Novo Name";
            var alteracaoEmail = "novo@email.com";
            var alteracaoPassword = "NovaPassword";
            var alteracaoRegistration = "NovaRegistration";

            var teste = new UserSystemEditDto
            {
                Id = 1,
                Name = alteracaoName,
                Email = alteracaoEmail,
                Password = alteracaoPassword,
                Situacao = false,
                Registration = alteracaoRegistration
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);
            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            var result = await userSystemController.Edit(teste);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Code);
            }

            var repository = new UserSystemRepository(context);
            var usuario = await repository.GetById(1);
            Assert.Equal(alteracaoName, usuario.Name);
            Assert.Equal(alteracaoEmail, usuario.Email);
            // Assert.Equal(alteracaoPassword, usuario.Password);
            Assert.Equal(alteracaoRegistration, usuario.Registration);
            Assert.False(usuario.Situacao);
        }

        [Fact]
        public async Task Edit_UserSystem_Erro()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_memoria_edit_usuario_sistema_Erro")
                .Options;

            var alteracaoName = "Novo Name";
            var alteracaoEmail = "novo@email.com";
            var alteracaoRegistration = "NovaRegistration";

            var teste = new UserSystemEditDto
            {
                Id = 1,
                Name = alteracaoName,
                Situacao = false
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            var result = await userSystemController.Edit(teste);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue.Code);
            }

            var repository = new UserSystemRepository(context);
            var usuario = await repository.GetById(1);
            Assert.NotEqual(alteracaoName, usuario.Name);
            Assert.NotEqual(alteracaoEmail, usuario.Email);
            Assert.NotEqual(alteracaoRegistration, usuario.Registration);
            Assert.True(usuario.Situacao);
        }
    }
}