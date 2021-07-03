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
        private readonly AirplaneValidateCodeRepetido _airplaneValidateCodeRepetido;
        private readonly IAirplaneRepository _repository;

        public AirplaneEditValidation(IAirplaneRepository repository,
            AirplaneValidateCodeRepetido airplaneValidateCodeRepetido)
            : base(repository)
        {
            _repository = repository;
            _airplaneValidateCodeRepetido = airplaneValidateCodeRepetido;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var registroExiste = await RegistroExiste(entity.Id);
            if (!registroExiste.Success) return registroExiste;

            var registroCodeRepetido = await _airplaneValidateCodeRepetido.Execute(entity);
            if (!registroCodeRepetido.Success) return registroCodeRepetido;

            registroCodeRepetido.Data = registroExiste.Data;

            return registroCodeRepetido;
        }
    }
}