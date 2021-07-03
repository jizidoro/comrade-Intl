#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Validations
{
    public class AirplaneValidateCodeRepetido : EntityValidation<Airplane>
    {
        private readonly IAirplaneRepository _repository;

        public AirplaneValidateCodeRepetido(IAirplaneRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var result = await _repository.RegistroCodeRepetido(entity.Id, entity.Code);

            return result;
        }
    }
}