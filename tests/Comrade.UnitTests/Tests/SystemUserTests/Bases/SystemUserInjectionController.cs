#region

using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.WebApi.UseCases.V2.SystemUserApi;

#endregion

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases
{
    public class SystemUserInjectionController
    {
        private readonly SystemUserInjectionService _systemUserInjectionService = new();

        public SystemUserController GetSystemUserController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();
            var systemUserCommand =
                _systemUserInjectionService.GetSystemUserCommand(context, mapper);
            var systemUserQuery = _systemUserInjectionService.GetSystemUserQuery(context, mapper);

            return new SystemUserController(systemUserCommand, systemUserQuery, mapper);
        }
    }
}