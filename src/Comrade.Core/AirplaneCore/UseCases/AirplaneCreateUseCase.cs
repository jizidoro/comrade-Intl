#region

using System;
using System.Threading.Tasks;
using comrade.Core.AirplaneCore.Validations;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.UseCases
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