#region

using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.WebApi.UseCases.V1.UsuarioSistemaApi;

#endregion

namespace Comrade.UnitTests.Tests.UsuarioSistemaTests.Bases
{
    public class UsuarioSistemaInjectionController
    {
        private readonly UsuarioSistemaInjectionAppService _usuarioSistemaInjectionAppService = new();

        public UsuarioSistemaController ObterUsuarioSistemaController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();
            var usuarioSistemaAppService =
                _usuarioSistemaInjectionAppService.ObterUsuarioSistemaAppService(context, mapper);

            return new UsuarioSistemaController(usuarioSistemaAppService, mapper);
        }
    }
}