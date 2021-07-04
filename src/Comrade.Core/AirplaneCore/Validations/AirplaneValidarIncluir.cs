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
        private readonly AirplaneValidateCodeRepeated _airplaneValidateCodeRepeated;
        private readonly IAirplaneRepository _repository;

        public AirplaneCreateValidation(IAirplaneRepository repository,
            AirplaneValidateCodeRepeated airplaneValidateCodeRepeated)
            : base(repository)
        {
            _repository = repository;
            _airplaneValidateCodeRepeated = airplaneValidateCodeRepeated;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            return await _airplaneValidateCodeRepeated.Execute(entity);
        }
    }
}