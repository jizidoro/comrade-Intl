#region

using System.Linq;
using Comrade.Core.Views.VBaUsuPermissaoCore;
using Comrade.Domain.Models.Views;
using Comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Comrade.Infrastructure.Repositories.Views
{
    public class VwUsuarioSistemaPermissaoRepository : IVwUsuarioSistemaPermissaoRepository
    {
        protected readonly ComradeContext Db;
        protected readonly DbSet<VwUsuarioSistemaPermissao> DbSet;

        public VwUsuarioSistemaPermissaoRepository(ComradeContext context)
        {
            Db = context;
            DbSet = Db.Set<VwUsuarioSistemaPermissao>();
        }


        public IQueryable<VwUsuarioSistemaPermissao> ListarPorSqUsuarioSistema(int sqUsuarioSistema)
        {
            var permissoes = Db.VUsuarioSistemaPermissoes
                .AsQueryable();

            return permissoes;
        }
    }
}