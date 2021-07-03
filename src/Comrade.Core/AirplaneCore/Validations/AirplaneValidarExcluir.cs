#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Validations
{
    public class AirplaneDeleteValidation : EntityValidation<Airplane>
    {
        private readonly IAirplaneRepository _repository;

        public AirplaneDeleteValidation(IAirplaneRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<Airplane>> Execute(int id)
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