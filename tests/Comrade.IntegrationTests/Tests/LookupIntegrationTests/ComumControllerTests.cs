#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Lookups;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UserSystemTests.Bases;
using comrade.WebApi.UseCases.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace comrade.IntegrationTests.Tests.LookupIntegrationTests
{
    public sealed class CommonControllerTests
    {
        private readonly GetServiceProviderDb _getServiceProviderDb = new();
        private readonly GetServiceProviderMemDb _getServiceProviderMemDb = new();
        private readonly ITestOutputHelper _output;
        private readonly UserSystemInjectionAppService _userSystemInjectionAppService = new();

        public CommonControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }


        private CommonController GetCommonControllerDb()
        {
            var mapper = MapperHelper.ConfigMapper();

            var serviceProvider = _getServiceProviderDb.Execute();

            var context = serviceProvider.GetService<ComradeContext>();

            var baUsuAppService = _userSystemInjectionAppService.GetUserSystemAppService(context, mapper);

            return new CommonController(serviceProvider, baUsuAppService);
        }

        private CommonController GetCommonControllerMemDb()
        {
            var mapper = MapperHelper.ConfigMapper();

            var serviceProvider = _getServiceProviderMemDb.Execute();

            var context = serviceProvider.GetService<ComradeContext>();

            var baUsuAppService = _userSystemInjectionAppService.GetUserSystemAppService(context, mapper);

            return new CommonController(serviceProvider, baUsuAppService);
        }

        [Fact(Skip = "local sql server")]
        public async Task GetLookupUserSystemDb_Test()
        {
            var commonController = GetCommonControllerDb();
            var result = await commonController.GetLookupUserSystem();

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
                Assert.NotNull(actualResultValue);
            }
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