#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.UnitTests.Tests.UsuarioSistemaTests.Bases;
using comrade.WebApi.UseCases.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace comrade.IntegrationTests.Tests.LookupIntegrationTests
{
    public sealed class ComumControllerTests
    {
        private readonly GetServiceProviderDb _obterServiceProviderDb = new();
        private readonly GetServiceProviderMemDb _obterServiceProviderMemDb = new();
        private readonly ITestOutputHelper _output;
        private readonly UserSystemInjectionAppService _userSystemInjectionAppService = new();

        public ComumControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }


        private ComumController GetComumControllerDb()
        {
            var mapper = MapperHelper.ConfigMapper();

            var serviceProvider = _obterServiceProviderDb.Execute();

            var context = serviceProvider.GetService<ComradeContext>();

            var baUsuAppService = _userSystemInjectionAppService.GetUserSystemAppService(context, mapper);

            return new ComumController(serviceProvider, baUsuAppService);
        }

        private ComumController GetComumControllerMemDb()
        {
            var mapper = MapperHelper.ConfigMapper();

            var serviceProvider = _obterServiceProviderMemDb.Execute();

            var context = serviceProvider.GetService<ComradeContext>();

            var baUsuAppService = _userSystemInjectionAppService.GetUserSystemAppService(context, mapper);

            return new ComumController(serviceProvider, baUsuAppService);
        }

        [Fact(Skip = "usa a instancia local do sqlserver")]
        public async Task GetLookupUserSystemDb_Test()
        {
            var comumController = GetComumControllerDb();
            var result = await comumController.GetLookupUserSystem();

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
                Assert.NotNull(actualResultValue);
            }
        }

        [Fact]
        public async Task GetLookupUserSystemMemDb_Test()
        {
            var comumController = GetComumControllerMemDb();
            var result = await comumController.GetLookupUserSystem();

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
                Assert.NotNull(actualResultValue);
            }
        }
    }
}