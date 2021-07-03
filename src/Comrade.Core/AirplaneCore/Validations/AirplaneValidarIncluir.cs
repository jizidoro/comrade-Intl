#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations
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