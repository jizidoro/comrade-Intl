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
    public class AirplaneDeleteUseCase : CoreCoreService, IAirplaneDeleteUseCase
    {
        private readonly AirplaneDeleteValidation _airplaneDeleteValidation;
        private readonly IAirplaneRepository _repository;

        public AirplaneDeleteUseCase(IAirplaneRepository repository,
            AirplaneDeleteValidation airplaneDeleteValidation,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneDeleteValidation = airplaneDeleteValidation;
        }

        public async Task<ISingleResult<Airplane>> Execute(int id)
        {
            var validate = await _airplaneDeleteValidation.Execute(id).ConfigureAwait(false);
            if (!validate.Success) return validate;

            _repository.Remove(id);

            _ = await Commit().ConfigureAwait(false);

            return new DeleteResult<Airplane>(true,
                BusinessMessage.ResourceManager.GetString("MSG03", CultureInfo.CurrentCulture));
        }
    }
}