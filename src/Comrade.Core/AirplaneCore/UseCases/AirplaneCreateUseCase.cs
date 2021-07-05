#region

using System;
using System.Globalization;
using System.Threading.Tasks;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.UseCases
{
    public class AirplaneCreateUseCase : Service
    {
        private readonly AirplaneCreateValidation _airplaneCreateValidation;
        private readonly IAirplaneRepository _repository;

        public AirplaneCreateUseCase(IAirplaneRepository repository, AirplaneCreateValidation airplaneCreateValidation,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneCreateValidation = airplaneCreateValidation;
        }

        public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
        {
            var isValid = ValidateEntity(entity);
            if (!isValid.Success)
            {
                return isValid;
            }

            var validate = await _airplaneCreateValidation.Execute(entity).ConfigureAwait(false);
            if (!validate.Success) return validate;
            entity.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();
            await _repository.Add(entity).ConfigureAwait(false);

            _ = await Commit().ConfigureAwait(false);

            return new CreateResult<Airplane>(true,
                BusinessMessage.ResourceManager.GetString("MSG01", CultureInfo.CurrentCulture));
        }
    }
}