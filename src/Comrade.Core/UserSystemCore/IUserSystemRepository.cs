#region

using System.Linq;
using comrade.Core.Helpers.Interfaces;
using comrade.Domain.Bases;
using comrade.Domain.Models;

#endregion

namespace comrade.Core.UserSystemCore
{
    public interface IUserSystemRepository : IRepository<UserSystem>
    {
        IQueryable<LookupEntity> FindByName(string name);
    }
}