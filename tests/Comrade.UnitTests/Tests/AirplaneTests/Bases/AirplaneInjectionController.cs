#region

using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.WebApi.UseCases.V1.AirplaneApi;
using Microsoft.Extensions.Logging;
using Moq;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases
{
    public class AirplaneInjectionController
    {
        private readonly AirplaneInjectionAppService _airplaneInjectionAppService = new();

        public AirplaneController GetAirplaneController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();

            var logger = Mock.Of<ILogger<AirplaneController>>();

            var airplaneAppService = _airplaneInjectionAppService.GetAirplaneAppService(context, mapper);

            return new AirplaneController(airplaneAppService, mapper, logger);
        }
    }
}