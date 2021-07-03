#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UserSystemCore.Validations
{
    public class UserSystemEditValidation : EntityValidation<UserSystem>
    {
        private readonly IUserSystemRepository _repository;

        public UserSystemEditValidation(IUserSystemRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<UserSystem>> Execute(UserSystem entity)
        {
            var registroExiste = await RegistroExiste(entity.Id);
            if (!registroExiste.Success)
            {
                return registroExiste;
            }

            return registroExiste;
        }
    }
}