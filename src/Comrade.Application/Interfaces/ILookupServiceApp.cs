#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Application.Interfaces
{
    public interface ILookupServiceApp<TEntity>
        where TEntity : Entity
    {
        Task<IList<LookupDto>> ObterLookup();
        Task<IList<LookupDto>> ObterLookup(Expression<Func<TEntity, bool>> predicate);
    }
}