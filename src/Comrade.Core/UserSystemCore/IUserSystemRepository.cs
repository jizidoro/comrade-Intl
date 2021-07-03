#region

using System.Linq;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UserSystemCore
{
    public interface IUserSystemRepository : IRepository<UserSystem>
    {
        IQueryable<LookupEntity> FindByName(string name);
    }
}