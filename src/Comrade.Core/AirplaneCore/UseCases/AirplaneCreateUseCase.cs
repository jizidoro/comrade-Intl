#region

using System;
using System.Threading.Tasks;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Results;
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
            try
            {
                var isValid = ValidateEntidade(entity);
                if (!isValid.Success)
                {
                    return isValid;
                }

                var validate = await _airplaneCreateValidation.Execute(entity);
                if (!validate.Success) return validate;
                entity.RegisterDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
                await _repository.Add(entity);

                var success = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<Airplane>(BusinessMessage.MSG07);
            }

            return new CreateResult<Airplane>(entity);
        }
    }
}