#region

using System.Threading.Tasks;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.Validations
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