﻿#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Application.Queries;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UserSystemTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace comrade.IntegrationTests.Tests.UserSystemIntegrationTests
{
    public class UserSystemControllerGetAllPaginatedTests
    {
        private readonly UserSystemInjectionController _userSystemInjectionController = new();

        [Fact]
        public async Task UserSystemController_GetAll_Paginated()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UserSystemController_GetAll_Paginated")
                .Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var userSystemController = _userSystemInjectionController.GetUserSystemController(context);
            var pagination = new PaginationQuery(1, 3);
            var result = await userSystemController.GetAll(pagination);

            if (result is OkObjectResult okObjectResult)
            {
                var actualResultValue = okObjectResult.Value as PageResultDto<UserSystemDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue.Code);
                Assert.NotNull(actualResultValue.Data);
                Assert.Equal(3, actualResultValue.Data.Count);
            }
        }
    }
}