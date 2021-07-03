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

namespace Comrade.Core.AirplaneCore.Usecases
{
    public class AirplaneExcluirUsecase : Service
    {
        private readonly AirplaneValidarExcluir _airplaneValidarExcluir;
        private readonly IAirplaneRepository _repository;

        public AirplaneExcluirUsecase(IAirplaneRepository repository, AirplaneValidarExcluir airplaneValidarExcluir,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneValidarExcluir = airplaneValidarExcluir;
        }

        public async Task<ISingleResult<Airplane>> Execute(int id)
        {
            try
            {
                var validacao = await _airplaneValidarExcluir.Execute(id);
                if (!validacao.Sucesso) return validacao;

                _repository.Remove(id);

                var sucesso = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<Airplane>(MensagensNegocio.MSG07);
            }

            return new ExcluirResult<Airplane>();
        }
    }
}