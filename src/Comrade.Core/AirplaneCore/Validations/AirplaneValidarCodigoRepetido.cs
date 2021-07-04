#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations
{
    public class AirplaneValidateCodeRepeated : EntityValidation<Airplane>
    {
        private readonly IAirplaneRepository _repository;

        public AirplaneValidateCodeRepeated(IAirplaneRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var result = await _repository.RegistroCodeRepeated(entity.Id, entity.Code);

            return result;
        }
    }
}