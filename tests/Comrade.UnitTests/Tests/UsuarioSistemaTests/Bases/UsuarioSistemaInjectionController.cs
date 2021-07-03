#region

using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.WebApi.UseCases.V1.UserSystemApi;

#endregion

namespace comrade.UnitTests.Tests.UsuarioSistemaTests.Bases
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