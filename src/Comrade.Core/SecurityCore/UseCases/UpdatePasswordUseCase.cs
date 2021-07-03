#region

using System;
using System.Threading.Tasks;
using comrade.Core.Helpers.Bases;
using comrade.Core.Helpers.Interfaces;
using comrade.Core.Helpers.Models.Results;
using comrade.Core.UserSystemCore;
using comrade.Core.UserSystemCore.Validations;
using comrade.Domain.Extensions;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.SecurityCore.UseCases
{
    public class UpdatePasswordUseCase : Service
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserSystemRepository _repository;
        private readonly UserSystemEditValidation _userSystemEditValidation;

        public UpdatePasswordUseCase(IUserSystemRepository repository,
            UserSystemEditValidation userSystemEditValidation,
            IPasswordHasher passwordHasher, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _userSystemEditValidation = userSystemEditValidation;
            _passwordHasher = passwordHasher;
        }

        public async Task<ISingleResult<UserSystem>> Execute(UserSystem entity)
        {
            try
            {
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
            target.Password = _passwordHasher.Hash(source.Password);
        }
    }
}