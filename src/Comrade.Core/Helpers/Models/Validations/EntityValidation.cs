#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Core.Helpers.Models.Validations
{
    public class EntityValidation<TEntity> : IEntityValidation<TEntity>
        where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;

        public EntityValidation(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<TEntity>> RecordExists(int id, params string[] includes)
        {
            var entity = await _repository.GetById(id, includes).ConfigureAwait(false);
            if (entity == null) return new SingleResult<TEntity>(BusinessMessage.MSG04);

            return new SingleResult<TEntity>(entity);
        }

        public async Task<ISingleResult<TEntity>> RegisterWithSameCode(int id, string code)
        {
            var result = await _repository.ValueExists(id, code).ConfigureAwait(false);
            if (result) return new SingleResult<TEntity>(BusinessMessage.MSG08);

            return new SingleResult<TEntity>();
        }
    }
}