#region

using System.Collections.Generic;
using Comrade.Application.Services;
using Comrade.Core.Helpers.Models;
using Comrade.Core.SecurityCore.Usecase;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.UsuarioSistemaCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.Infrastructure.Repositories.Views;
using Comrade.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;

#endregion

namespace Comrade.UnitTests.Tests.AutenticacaoTests.Bases
{
    public sealed class AutenticacaoInjectionUseCase
    {
        public AtualizarSenhaExpiradaUsecase ObterAtualizarSenhaExpiradaUsecase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var usuarioSistemaCoreRepository = new UsuarioSistemaRepository(context);

            var usuarioSistemaCoreValidarEditar =
                new UsuarioSistemaValidarEditar(usuarioSistemaCoreRepository
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new AtualizarSenhaExpiradaUsecase(usuarioSistemaCoreRepository,
                usuarioSistemaCoreValidarEditar, passwordHasher, uow);
        }

        public EsquecerSenhaUsecase ObterEsquecerSenhaUsecase(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var usuarioSistemaCoreRepository = new UsuarioSistemaRepository(context);
            var usuarioSistemaValidarEditar = new UsuarioSistemaValidarEditar(usuarioSistemaCoreRepository);
            var usuarioSistemaValidarEsquecerSenha =
                new UsuarioSistemaValidarEsquecerSenha(usuarioSistemaCoreRepository, usuarioSistemaValidarEditar
                );
            var passwordHasher = new PasswordHasher(new HashingOptions());

            return new EsquecerSenhaUsecase(usuarioSistemaCoreRepository, usuarioSistemaValidarEsquecerSenha,
                passwordHasher, uow);
        }

        public GerarTokenLoginUsecase ObterGerarTokenLoginUsecase(ComradeContext context)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:Key", "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var uow = new UnitOfWork(context);
            var usuarioSistemaCoreRepository = new UsuarioSistemaRepository(context);

            var passwordHasher = new PasswordHasher(new HashingOptions());

            var usuarioSistemaValidarSenha =
                new UsuarioSistemaValidarSenha(usuarioSistemaCoreRepository, passwordHasher);
            var vUsuarioSistemaPermissaoRepository =
                new VwUsuarioSistemaPermissaoRepository(context);
            var gerarTokenLoginUsecase =
                new GerarTokenLoginUsecase
                (
                    configuration,
                    usuarioSistemaValidarSenha
                );
            return gerarTokenLoginUsecase;
        }

        private AutenticacaoAppService ObterUsuarioSistemaAppService(ComradeContext context)
        {
            var uow = new UnitOfWork(context);
            var vUsuarioSistemaRepository = new VwUsuarioSistemaPermissaoRepository(context);
            var mapper = MapperHelper.ConfigMapper();

            var oterAtualizarSenhaExpiradaUsecase = ObterAtualizarSenhaExpiradaUsecase(context);
            var obterEsquecerSenhaUsecase = ObterEsquecerSenhaUsecase(context);
            var obterGerarTokenLoginUsecaseUsecase = ObterGerarTokenLoginUsecase(context);

            var autenticacaoAppService = new AutenticacaoAppService(vUsuarioSistemaRepository,
                oterAtualizarSenhaExpiradaUsecase,
                obterGerarTokenLoginUsecaseUsecase, obterEsquecerSenhaUsecase, mapper);
            return autenticacaoAppService;
        }
    }
}