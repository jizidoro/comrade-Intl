#region

using System;
using System.Linq;
using System.Threading.Tasks;
using Comrade.Core.AirplaneCore;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Bases;
using Comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Comrade.Infrastructure.Repositories
{
    public class AirplaneRepository : Repository<Airplane>, IAirplaneRepository
    {
        private readonly ComradeContext _context;

        public AirplaneRepository(ComradeContext context)
            : base(context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }

        public async Task<ISingleResult<Airplane>> ValidateSameCode(int id, string code)
        {
            var exists = await _context.Airplanes
                .Where(p => p.Id != id && p.Code.Equals(code, StringComparison.Ordinal))
                .AnyAsync().ConfigureAwait(false);

            return exists
                ? new SingleResult<Airplane>(BusinessMessage.MSG08)
                : new SingleResult<Airplane>();
        }
    }
}