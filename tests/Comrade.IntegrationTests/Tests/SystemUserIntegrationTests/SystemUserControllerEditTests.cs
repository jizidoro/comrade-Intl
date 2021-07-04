#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.SystemUserDtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests
{
    public class SystemUserControllerEditTests
    {
        private readonly SystemUserInjectionController _systemUserInjectionController = new();

        [Fact]
        public async Task SystemUserController_Edit()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_SystemUserController_Edit")
                .Options;

            var changeName = "Novo Name";
            var changeEmail = "novo@email.com";
            var changePassword = "NovaPassword";
            var changeRegistration = "NovaRegistration";

            var testObject = new SystemUserEditDto
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
            var systemUserController = _systemUserInjectionController.GetSystemUserController(context);
            var result = await systemUserController.Edit(testObject);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Code);
            }

            var repository = new SystemUserRepository(context);
            var user = await repository.GetById(1);
            Assert.Equal(changeName, user.Name);
            Assert.Equal(changeEmail, user.Email);
            // Assert.Equal(changePassword, tokenUser.Password);
            Assert.Equal(changeRegistration, user.Registration);
            Assert.False(user.Situacao);
        }

        [Fact]
        public async Task Edit_SystemUser_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_Edit_SystemUser_Error")
                .Options;

            var changeName = "Novo Name";
            var changeEmail = "novo@email.com";
            var changeRegistration = "NovaRegistration";

            var testObject = new SystemUserEditDto
            {
                Id = 1,
                Name = changeName,
                Situacao = false
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var systemUserController = _systemUserInjectionController.GetSystemUserController(context);
            var result = await systemUserController.Edit(testObject);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue.Code);
            }

            var repository = new SystemUserRepository(context);
            var user = await repository.GetById(1);
            Assert.NotEqual(changeName, user.Name);
            Assert.NotEqual(changeEmail, user.Email);
            Assert.NotEqual(changeRegistration, user.Registration);
            Assert.True(user.Situacao);
        }
    }
}