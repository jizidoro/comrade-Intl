#region

using System.Linq;
using Comrade.Core.Views.VBaUsuPermissaoCore;
using Comrade.Domain.Models.Views;
using Comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Comrade.Infrastructure.Repositories.Views
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