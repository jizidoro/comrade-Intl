#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Validations
{
    public class AirplaneCreateValidation : EntityValidation<Airplane>
    {
        private readonly AirplaneValidateCodeRepetido _airplaneValidateCodeRepetido;
        private readonly IAirplaneRepository _repository;

        public AirplaneCreateValidation(IAirplaneRepository repository,
            AirplaneValidateCodeRepetido airplaneValidateCodeRepetido)
            : base(repository)
        {
            _repository = repository;
            _airplaneValidateCodeRepetido = airplaneValidateCodeRepetido;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            return await _airplaneValidateCodeRepetido.Execute(entity);
        }
    }
}