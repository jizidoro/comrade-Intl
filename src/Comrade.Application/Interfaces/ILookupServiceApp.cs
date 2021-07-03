#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Domain.Bases;

#endregion

namespace comrade.Application.Interfaces
{
    public interface ILookupServiceApp<TEntity>
        where TEntity : Entity
    {
        Task<IList<LookupDto>> GetLookup();
        Task<IList<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate);
    }
}