#region

using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.WebApi.UseCases.V1.LoginApi;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases
{
    public class TokenInjectionController
    {
        private readonly AuthenticationInjectionAppService _authenticationInjectionAppService = new();

        public TokenController GetTokenController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();

            var authenticationAppService =
                _authenticationInjectionAppService.GetAuthenticationAppService(context, mapper);

            return new TokenController(authenticationAppService);
        }
    }
}