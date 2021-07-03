#region

using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.WebApi.UseCases.V1.UserSystemApi;

#endregion

namespace Comrade.UnitTests.Tests.UserSystemTests.Bases
{
    public class UserSystemInjectionController
    {
        private readonly UserSystemInjectionAppService _userSystemInjectionAppService = new();

        public UserSystemController GetUserSystemController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();
            var userSystemAppService =
                _userSystemInjectionAppService.GetUserSystemAppService(context, mapper);

            return new UserSystemController(userSystemAppService, mapper);
        }
    }
}