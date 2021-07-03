#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.UsuarioSistemaCore;
using Comrade.Core.UsuarioSistemaCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore.Usecase
{
    public class EsquecerSenhaUsecase : Service
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsuarioSistemaRepository _repository;
        private readonly UsuarioSistemaValidarEsquecerSenha _usuarioSistemaValidarEsquecerSenha;

        public EsquecerSenhaUsecase(IUsuarioSistemaRepository repository,
            UsuarioSistemaValidarEsquecerSenha usuarioSistemaValidarEsquecerSenha,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _usuarioSistemaValidarEsquecerSenha = usuarioSistemaValidarEsquecerSenha;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<UsuarioSistema>> Execute(UsuarioSistema entity)
        {
            try
            {
                var result = _usuarioSistemaValidarEsquecerSenha.Execute(entity);
                if (!result.Sucesso) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                var sucesso = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<UsuarioSistema>(ex);
            }

            return new EditarResult<UsuarioSistema>();
        }

        private void HydrateValues(UsuarioSistema target, UsuarioSistema source)
        {
            var regraEsquecerSenha = "123456";
            target.Senha = _passwordHasher.Hash(regraEsquecerSenha);
        }
    }
}