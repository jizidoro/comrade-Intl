#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UsuarioSistemaCore.Validations
{
    public class UsuarioSistemaValidarEditar : EntityValidation<UsuarioSistema>
    {
        private readonly IUsuarioSistemaRepository _repository;

        public UsuarioSistemaValidarEditar(IUsuarioSistemaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<UsuarioSistema>> Execute(UsuarioSistema entity)
        {
            var registroExiste = await RegistroExiste(entity.Id);
            if (!registroExiste.Sucesso)
            {
                return registroExiste;
            }

            return registroExiste;
        }
    }
}