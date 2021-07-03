#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.UserSystemDtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.UserSystemTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.UserSystemIntegrationTests
{
    public class UserSystemControllerEditTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystemController_Edit()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UserSystemController_Edit")
                .Options;

            var changeName = "Novo Name";
            var changeEmail = "novo@email.com";
            var changePassword = "NovaPassword";
            var changeRegistration = "NovaRegistration";

            var testObject = new UserSystemEditDto
            {
                Id = 1,
                Name = changeName,
                Email = changeEmail,
                Password = changePassword,
                Situacao = false,
                Registration = changeRegistration
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);
            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            var result = await userSystemController.Edit(testObject);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Code);
            }

            var repository = new UserSystemRepository(context);
            var user = await repository.GetById(1);
            Assert.Equal(changeName, user.Name);
            Assert.Equal(changeEmail, user.Email);
            // Assert.Equal(changePassword, user.Password);
            Assert.Equal(changeRegistration, user.Registration);
            Assert.False(user.Situacao);
        }

        [Fact]
        public async Task Edit_UserSystem_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_Edit_UserSystem_Error")
                .Options;

            var changeName = "Novo Name";
            var changeEmail = "novo@email.com";
            var changeRegistration = "NovaRegistration";

            var testObject = new UserSystemEditDto
            {
                Id = 1,
                Name = changeName,
                Situacao = false
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            var result = await userSystemController.Edit(testObject);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue.Code);
            }

            var repository = new UserSystemRepository(context);
            var user = await repository.GetById(1);
            Assert.NotEqual(changeName, user.Name);
            Assert.NotEqual(changeEmail, user.Email);
            Assert.NotEqual(changeRegistration, user.Registration);
            Assert.True(user.Situacao);
        }
    }
}