﻿#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Lookups;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Comrade.WebApi.UseCases.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.IntegrationTests.Tests.LookupIntegrationTests
{
    public sealed class CommonControllerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly SystemUserInjectionService _systemUserInjectionService = new();

        public CommonControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }


        private CommonController GetCommonControllerMemDb()
        {
            var mapper = MapperHelper.ConfigMapper();
            var serviceProvider = GetServiceProviderMemDb.Execute();
            var context = serviceProvider.GetService<ComradeContext>();
            var systemUserQuery = _systemUserInjectionService.GetSystemUserQuery(context!, mapper);

            return new CommonController(serviceProvider, systemUserQuery);
        }

        [Fact]
        public async Task GetLookupSystemUserMemDb_Test()
        {
            var commonController = GetCommonControllerMemDb();
            var result = await commonController.GetLookupSystemUser();

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
                Assert.NotNull(actualResultValue);
                Assert.NotNull(actualResultValue?.Data);
                Assert.Equal(4, actualResultValue?.Data?.Count);
            }
        }

        [Fact]
        public async Task GetLookupSystemUserPredicateMemDb_Test()
        {
            var commonController = GetCommonControllerMemDb();
            var result = await commonController.GetLookupPredicateSystemUserByName("aa");

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
                Assert.NotNull(actualResultValue);
                Assert.NotNull(actualResultValue?.Data);
                Assert.Equal(1, actualResultValue?.Data?.Count);
            }
        }

        [Fact]
        public async Task GetLookupSystemUserByName_Test()
        {
            var commonController = GetCommonControllerMemDb();
            var result = await commonController.GetLookupSystemUserByName("aa");

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
                Assert.NotNull(actualResultValue);
                Assert.NotNull(actualResultValue?.Data);
                Assert.Equal(1, actualResultValue?.Data?.Count);
            }
        }
    }
}