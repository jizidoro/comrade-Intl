#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Domain.Interfaces;

#endregion

namespace Comrade.Core.Helpers.Bases
{
    public class Service : IService
    {
        private readonly IUnitOfWork _uow;

        public Service(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Commit()
        {
            if (await _uow.Commit().ConfigureAwait(false)) return true;

            return false;
        }

        public static ISingleResult<T> ValidateEntity<T>(T entity) where T : IEntity
        {
            var context = new ValidationContext(entity, null, null);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(entity, context, validationResults, true);
            if (!valid)
            {
                var listErrors = validationResults.Select(x => x.ErrorMessage);
                return new SingleResult<T>(listErrors!);
            }

            return new SingleResult<T>();
        }
    }
}