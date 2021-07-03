#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.UserSystemCore.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UserSystemCore.UseCases
{
    public class UserSystemDeleteUseCase : Service
    {
        private readonly IUserSystemRepository _repository;
        private readonly UserSystemDeleteValidation _userSystemDeleteValidation;

        public UserSystemDeleteUseCase(IUserSystemRepository repository,
            UserSystemDeleteValidation userSystemDeleteValidation,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _userSystemDeleteValidation = userSystemDeleteValidation;
        }

        public async Task<ISingleResult<UserSystem>> Execute(int id)
        {
            try
            {
                var validate = await _userSystemDeleteValidation.Execute(id);
                if (!validate.Success) return validate;

                _repository.Remove(id);

                var success = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<UserSystem>(BusinessMessage.MSG07);
            }

            return new DeleteResult<UserSystem>();
        }
    }
}