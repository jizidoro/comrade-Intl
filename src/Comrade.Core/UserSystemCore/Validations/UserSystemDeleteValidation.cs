#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UserSystemCore.Validations
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