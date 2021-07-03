﻿#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Lookups;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.UserSystemTests.Bases;
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
        private readonly UserSystemInjectionAppService _userSystemInjectionAppService = new();

        public CommonControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }


        private CommonController GetCommonControllerMemDb()
        {
            var mapper = MapperHelper.ConfigMapper();

            var serviceProvider = GetServiceProviderMemDb.Execute();

            var context = serviceProvider.GetService<ComradeContext>();

            var baUsuAppService = _userSystemInjectionAppService.GetUserSystemAppService(context, mapper);

            return new CommonController(serviceProvider, baUsuAppService);
        }

        [Fact]
        public async Task GetLookupUserSystemMemDb_Test()
        {
            var commonController = GetCommonControllerMemDb();
            var result = await commonController.GetLookupUserSystem();

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
                Assert.NotNull(actualResultValue);
            }
        }
    }
}