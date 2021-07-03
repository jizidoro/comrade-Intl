#region

using System;
using System.Threading.Tasks;
using comrade.Core.AirplaneCore.Validations;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.AirplaneCore.UseCases
{
    public class AirplaneEditUseCase : Service
    {
        private readonly AirplaneEditValidation _airplaneEditValidation;
        private readonly IAirplaneRepository _repository;

        public AirplaneEditUseCase(IAirplaneRepository repository, AirplaneEditValidation airplaneEditValidation,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneEditValidation = airplaneEditValidation;
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

                var result = await _airplaneEditValidation.Execute(entity);
                if (!result.Success) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                _ = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<Airplane>(ex);
            }

            return new EditResult<Airplane>();
        }

        private void HydrateValues(Airplane target, Airplane source)
        {
            target.Code = source.Code;
            target.QuantidadePassageiro = source.QuantidadePassageiro;
            target.Model = source.Model;
        }
    }
}