#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.UsuarioSistemaCore.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UsuarioSistemaCore.Usecases
{
    public class UsuarioSistemaExcluirUsecase : Service
    {
        private readonly IUsuarioSistemaRepository _repository;
        private readonly UsuarioSistemaValidarExcluir _usuarioSistemaValidarExcluir;

        public UsuarioSistemaExcluirUsecase(IUsuarioSistemaRepository repository,
            UsuarioSistemaValidarExcluir usuarioSistemaValidarExcluir,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _usuarioSistemaValidarExcluir = usuarioSistemaValidarExcluir;
        }

        public async Task<ISingleResult<UsuarioSistema>> Execute(int id)
        {
            try
            {
                var validacao = await _usuarioSistemaValidarExcluir.Execute(id);
                if (!validacao.Sucesso) return validacao;

                _repository.Remove(id);

                var sucesso = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<UsuarioSistema>(MensagensNegocio.MSG07);
            }

            return new ExcluirResult<UsuarioSistema>();
        }
    }
}