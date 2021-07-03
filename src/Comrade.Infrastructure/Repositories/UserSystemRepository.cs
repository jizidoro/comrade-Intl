#region

using System;
using System.Linq;
using Comrade.Core.UserSystemCore;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Bases;
using Comrade.Infrastructure.DataAccess;

#endregion

namespace Comrade.Infrastructure.Repositories
{
    public class UserSystemRepository : Repository<UserSystem>, IUserSystemRepository
    {
        private readonly ComradeContext _context;

        public UserSystemRepository(ComradeContext context)
            : base(context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }


        public IQueryable<LookupEntity> FindByName(string name)
        {
            var result = Db.UserSystems
                .Where(x => x.Situacao &&
                            x.Name.Contains(name)).Take(30)
                .OrderBy(x => x.Name)
                .Select(s => new LookupEntity {Key = s.Id, Value = s.Name});

            return result;
        }
    }
}