#region

using System;
using System.Threading.Tasks;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.UserSystemCore.Validations;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UserSystemCore.UseCases
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