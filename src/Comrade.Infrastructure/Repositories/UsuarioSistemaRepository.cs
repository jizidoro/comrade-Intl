#region

using System;
using System.Linq;
using Comrade.Core.UsuarioSistemaCore;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Bases;
using Comrade.Infrastructure.DataAccess;

#endregion

namespace Comrade.Infrastructure.Repositories
{
    public class UsuarioSistemaRepository : Repository<UsuarioSistema>, IUsuarioSistemaRepository
    {
        private readonly ComradeContext _context;

        public UsuarioSistemaRepository(ComradeContext context)
            : base(context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }


        public IQueryable<LookupEntity> BuscarPorNome(string nome)
        {
            var result = Db.UsuarioSistemas
                .Where(x => x.Situacao &&
                            x.Nome.Contains(nome)).Take(30)
                .OrderBy(x => x.Nome)
                .Select(s => new LookupEntity {Key = s.Id, Value = s.Nome});

            return result;
        }
    }
}