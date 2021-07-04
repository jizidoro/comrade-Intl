#region

using System.Linq;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore
{
    public interface ISystemUserRepository : IRepository<SystemUser>
    {
        IQueryable<LookupEntity> FindByName(string name);
    }
}