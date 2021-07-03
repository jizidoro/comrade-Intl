#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations
{
    public class AirplaneValidarIncluir : EntityValidation<Airplane>
    {
        private readonly AirplaneValidarCodigoRepetido _airplaneValidarCodigoRepetido;
        private readonly IAirplaneRepository _repository;

        public AirplaneValidarIncluir(IAirplaneRepository repository,
            AirplaneValidarCodigoRepetido airplaneValidarCodigoRepetido)
            : base(repository)
        {
            _repository = repository;
            _airplaneValidarCodigoRepetido = airplaneValidarCodigoRepetido;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            return await _airplaneValidarCodigoRepetido.Execute(entity);
        }
    }
}