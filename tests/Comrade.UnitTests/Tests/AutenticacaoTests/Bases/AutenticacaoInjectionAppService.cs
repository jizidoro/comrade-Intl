#region

using Comrade.Application.Services;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories.Views;
using Comrade.UnitTests.Helpers;

#endregion

namespace Comrade.UnitTests.Tests.AutenticacaoTests.Bases
{
    public class AutenticacaoInjectionAppService
    {
        private readonly AutenticacaoInjectionUseCase _autenticacaoInjectionUseCase = new();

        public AutenticacaoAppService ObterAutenticacaoAppServiceService(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var vUsuarioSistemaRepository = new VwUsuarioSistemaPermissaoRepository(context);
            var mapper = MapperHelper.ConfigMapper();

            var oterAtualizarSenhaExpiradaUsecase =
                _autenticacaoInjectionUseCase.ObterAtualizarSenhaExpiradaUsecase(context);
            var obterEsquecerSenhaUsecase =
                _autenticacaoInjectionUseCase.ObterEsquecerSenhaUsecase(context);
            var obterGerarTokenLoginUsecaseUsecase =
                _autenticacaoInjectionUseCase.ObterGerarTokenLoginUsecase(context);

            var autenticacaoAppService = new AutenticacaoAppService(vUsuarioSistemaRepository,
                oterAtualizarSenhaExpiradaUsecase,
                obterGerarTokenLoginUsecaseUsecase, obterEsquecerSenhaUsecase, mapper);
            return autenticacaoAppService;
        }
    }
}