#region

using Comrade.Domain.Interfaces;

#endregion

namespace Comrade.Core.Helpers.Interfaces
{
    public interface ISingleResult<TEntity> : IResult
        where TEntity : IEntity
    {
        TEntity Data { get; set; }
    }
}