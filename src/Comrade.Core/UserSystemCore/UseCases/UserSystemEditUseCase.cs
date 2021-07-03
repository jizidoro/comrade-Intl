#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.UserSystemCore.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UserSystemCore.UseCases
{
    public class UserSystemEditUseCase : Service
    {
        private readonly IUserSystemRepository _repository;
        private readonly UserSystemEditValidation _userSystemEditValidation;

        public UserSystemEditUseCase(IUserSystemRepository repository,
            UserSystemEditValidation userSystemEditValidation, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _userSystemEditValidation = userSystemEditValidation;
        }

        public async Task<ISingleResult<UserSystem>> Execute(UserSystem entity)
        {
            try
            {
                var isValid = ValidateEntidade(entity);
                if (!isValid.Success)
                {
                    return isValid;
                }

                var result = await _userSystemEditValidation.Execute(entity);
                if (!result.Success) return result;

                var obj = result.Data;

                HydrateValues(obj, entity);

                _repository.Update(obj);

                var success = await Commit();
            }
            catch (Exception ex)
            {
                return new SingleResult<UserSystem>(ex);
            }

            return new EditResult<UserSystem>();
        }

        private void HydrateValues(UserSystem target, UserSystem source)
        {
            target.Name = source.Name;
            target.Email = source.Email;
            target.Registration = source.Registration;
            target.Situacao = source.Situacao;
        }
    }
}