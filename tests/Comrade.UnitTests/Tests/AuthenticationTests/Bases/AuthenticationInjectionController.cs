#region

using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.WebApi.UseCases.V1.LoginApi;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases
{
    public class AuthenticationInjectionController
    {
        private readonly AuthenticationInjectionAppService _authenticationInjectionAppService = new();

        public AuthenticationController GetAuthenticationController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();

            var authenticationAppService =
                _authenticationInjectionAppService.GetAuthenticationAppService(context, mapper);

            return new AuthenticationController(authenticationAppService);
        }
    }
}