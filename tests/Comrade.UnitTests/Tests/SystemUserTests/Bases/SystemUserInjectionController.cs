#region

using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.WebApi.UseCases.V2.SystemUserApi;

#endregion

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases
{
    public class SystemUserInjectionController
    {
        private readonly SystemUserInjectionAppService _systemUserInjectionAppService = new();

        public SystemUserController GetSystemUserController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();
            var systemUserAppService =
                _systemUserInjectionAppService.GetSystemUserAppService(context, mapper);

            return new SystemUserController(systemUserAppService, mapper);
        }
    }
}