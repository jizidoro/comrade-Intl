#region

using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UsuarioSistemaCore.Validations
{
    public class UsuarioSistemaValidarIncluir : EntityValidation<UsuarioSistema>
    {
        private readonly IUsuarioSistemaRepository _repository;

        public UsuarioSistemaValidarIncluir(IUsuarioSistemaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ISingleResult<UsuarioSistema> Execute(UsuarioSistema entity)
        {
            return new SingleResult<UsuarioSistema>(entity);
        }
    }
}