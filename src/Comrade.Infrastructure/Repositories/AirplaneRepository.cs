#region

using System;
using System.Linq;
using System.Threading.Tasks;
using comrade.Core.AirplaneCore;
using comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using comrade.Core.Helpers.Models.Results;
using comrade.Domain.Models;
using comrade.Infrastructure.Bases;
using comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

#endregion

namespace comrade.Infrastructure.Repositories
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

        public async Task<ISingleResult<Airplane>> RegistroCodeRepetido(int id, string codigo)
        {
            var existe = await Db.Airplanes
                .Where(p => p.Id != id && p.Code.Equals(codigo))
                .AnyAsync();

            return existe ? new SingleResult<Airplane>(BusinessMessage.MSG08) : new SingleResult<Airplane>();
        }
    }
}