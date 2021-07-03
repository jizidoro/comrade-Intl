#region

using System.Linq;
using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.UserSystemDtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Tests.UserSystemTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.UserSystemIntegrationTests
{
    public sealed class UserSystemControllerCreateTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystemController_Create()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UserSystemController_Create")
                .Options;


            var testObject = new UserSystemCreateDto
            {
                Name = "111",
                Email = "777@testObject",
                Password = "123456",
                Situacao = true,
                Registration = "123"
            };


            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            _ = await userSystemController.Create(testObject);
            Assert.Equal(1, context.UserSystems.Count());
        }


        [Fact]
        public async Task UserSystemController_Create_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UserSystemController_Create_Error")
                .Options;

            var testObject = new UserSystemCreateDto
            {
                Email = "777@testObject",
                Password = "123456",
                Situacao = true,
                Registration = "123"
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            var result = await userSystemController.Create(testObject);

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