#region

using AutoMapper;
using Comrade.Application.Services;
using Comrade.Core.Helpers.Models;
using Comrade.Core.UsuarioSistemaCore.Usecases;
using Comrade.Core.UsuarioSistemaCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;

#endregion

namespace Comrade.UnitTests.Tests.UsuarioSistemaTests.Bases
{
    public sealed class UsuarioSistemaInjectionAppService
    {
        public UsuarioSistemaAppService ObterUsuarioSistemaAppService(ComradeContext context, IMapper mapper)
        {
            var uow = new UnitOfWork(context);
            var usuarioSistemaRepository = new UsuarioSistemaRepository(context);
            var passwordHasher = new PasswordHasher(new HashingOptions());

            var usuarioSistemaValidarEditar =
                new UsuarioSistemaValidarEditar(usuarioSistemaRepository);
            var usuarioSistemaValidarExcluir = new UsuarioSistemaValidarExcluir(usuarioSistemaRepository);
            var usuarioSistemaValidarIncluir =
                new UsuarioSistemaValidarIncluir(usuarioSistemaRepository);
            var usuarioSistemaIncluirUsecase =
                new UsuarioSistemaIncluirUsecase(usuarioSistemaRepository, usuarioSistemaValidarIncluir, passwordHasher,
                    uow);
            var usuarioSistemaExcluirUsecase =
                new UsuarioSistemaExcluirUsecase(usuarioSistemaRepository, usuarioSistemaValidarExcluir, uow);
            var usuarioSistemaEditarUsecase =
                new UsuarioSistemaEditarUsecase(usuarioSistemaRepository, usuarioSistemaValidarEditar, uow);

            return new UsuarioSistemaAppService(usuarioSistemaRepository, usuarioSistemaEditarUsecase,
                usuarioSistemaIncluirUsecase,
                usuarioSistemaExcluirUsecase, mapper);
        }
    }
}