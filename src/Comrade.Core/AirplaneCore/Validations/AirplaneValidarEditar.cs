#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations
{
    public class AirplaneValidarEditar : EntityValidation<Airplane>
    {
        private readonly AirplaneValidarCodigoRepetido _airplaneValidarCodigoRepetido;
        private readonly IAirplaneRepository _repository;

        public AirplaneValidarEditar(IAirplaneRepository repository,
            AirplaneValidarCodigoRepetido airplaneValidarCodigoRepetido)
            : base(repository)
        {
            _repository = repository;
            _airplaneValidarCodigoRepetido = airplaneValidarCodigoRepetido;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var registroExiste = await RegistroExiste(entity.Id);
            if (!registroExiste.Sucesso) return registroExiste;

            var registroCodigoRepetido = await _airplaneValidarCodigoRepetido.Execute(entity);
            if (!registroCodigoRepetido.Sucesso) return registroCodigoRepetido;

            registroCodigoRepetido.Data = registroExiste.Data;

            return registroCodigoRepetido;
        }
    }
}