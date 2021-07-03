#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UserSystemCore.Validations
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