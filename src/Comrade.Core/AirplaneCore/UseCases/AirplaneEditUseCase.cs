#region

using System.Globalization;
using System.Threading.Tasks;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.UseCases
{
    public class AirplaneEditUseCase : CoreCoreService, IAirplaneEditUseCase
    {
        private readonly AirplaneEditValidation _airplaneEditValidation;
        private readonly IAirplaneRepository _repository;

        public AirplaneEditUseCase(IAirplaneRepository repository,
            AirplaneEditValidation airplaneEditValidation,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneEditValidation = airplaneEditValidation;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var isValid = ValidateEntity(entity);
            if (!isValid.Success)
            {
                return isValid;
            }

            var result = await _airplaneEditValidation.Execute(entity).ConfigureAwait(false);
            if (!result.Success) return result;

            var obj = result.Data!;

            HydrateValues(obj, entity);

            _repository.Update(obj);

            _ = await Commit().ConfigureAwait(false);

            return new EditResult<Airplane>(true,
                BusinessMessage.ResourceManager.GetString("MSG02", CultureInfo.CurrentCulture));
        }

        private static void HydrateValues(Airplane target, Airplane source)
        {
            target.Code = source.Code;
            target.PassengerQuantity = source.PassengerQuantity;
            target.Model = source.Model;
        }
    }
}