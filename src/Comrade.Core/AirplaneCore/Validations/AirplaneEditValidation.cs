#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations
{
    public class AirplaneEditValidation : EntityValidation<Airplane>
    {
        private readonly AirplaneValidateSameCode _airplaneValidateSameCode;
        private readonly IAirplaneRepository _repository;

        public AirplaneEditValidation(IAirplaneRepository repository,
            AirplaneValidateSameCode airplaneValidateSameCode)
            : base(repository)
        {
            _repository = repository;
            _airplaneValidateSameCode = airplaneValidateSameCode;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var recordExists = await RecordExists(entity.Id);
            if (!recordExists.Success)
            {
                return recordExists;
            }

            var registerSameCode = await _airplaneValidateSameCode.Execute(entity);
            if (!registerSameCode.Success)
            {
                return registerSameCode;
            }

            return recordExists;
        }
    }
}