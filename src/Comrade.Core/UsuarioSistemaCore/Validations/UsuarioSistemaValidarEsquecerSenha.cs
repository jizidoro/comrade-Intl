#region

using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UsuarioSistemaCore.Validations
{
    public class UsuarioSistemaValidarEsquecerSenha : EntityValidation<UsuarioSistema>
    {
        private readonly IUsuarioSistemaRepository _repository;
        private readonly UsuarioSistemaValidarEditar _usuarioSistemaValidarEditar;

        public UsuarioSistemaValidarEsquecerSenha(IUsuarioSistemaRepository repository,
            UsuarioSistemaValidarEditar usuarioSistemaValidarEditar)
            : base(repository)
        {
            _repository = repository;
            _usuarioSistemaValidarEditar = usuarioSistemaValidarEditar;
        }

        public ISingleResult<UsuarioSistema> Execute(UsuarioSistema entity)
        {
            var registroExiste = _usuarioSistemaValidarEditar.Execute(entity).Result;

            if (!registroExiste.Sucesso)
            {
                return new SingleResult<UsuarioSistema>(1001, "Usuario não existe");
            }


            return registroExiste;
        }
    }
}