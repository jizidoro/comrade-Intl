#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UserSystemCore.Validations
{
    public class UserSystemDeleteValidation : EntityValidation<UserSystem>
    {
        private readonly IUserSystemRepository _repository;

        public UserSystemDeleteValidation(IUserSystemRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<UserSystem>> Execute(int id)
        {
            var registroExiste = await RegistroExiste(id);
            if (!registroExiste.Success)
            {
                return registroExiste;
            }

            return registroExiste;
        }
    }
}