#region

using System.Threading.Tasks;
using Comrade.Domain.Interfaces;

#endregion

namespace Comrade.Core.Helpers.Interfaces
{
    public interface IEntityValidation<TEntity>
        where TEntity : IEntity
    {
        Task<ISingleResult<TEntity>> RegistroExiste(int id, params string[] includes);

        Task<ISingleResult<TEntity>> RegistroComMesmoCodigo(int id, string codigo);
    }
}