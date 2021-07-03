#region

using System.Linq;
using comrade.Core.Views.VBaUsuPermissaoCore;
using comrade.Domain.Models.Views;
using comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

#endregion

namespace comrade.Infrastructure.Repositories.Views
{
    public class VwUserSystemPermissionRepository : IVwUserSystemPermissionRepository
    {
        protected readonly ComradeContext Db;
        protected readonly DbSet<VwUserSystemPermission> DbSet;

        public VwUserSystemPermissionRepository(ComradeContext context)
        {
            Db = context;
            DbSet = Db.Set<VwUserSystemPermission>();
        }


        public IQueryable<VwUserSystemPermission> GetAllBySqUserSystem(int sqUserSystem)
        {
            var permissoes = Db.VUserSystemPermissoes
                .AsQueryable();

            return permissoes;
        }
    }
}