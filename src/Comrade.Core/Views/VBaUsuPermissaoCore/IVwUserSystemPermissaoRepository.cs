#region

using System.Linq;
using comrade.Domain.Models.Views;

#endregion

namespace comrade.Core.Views.VBaUsuPermissaoCore
{
    public interface IVwUserSystemPermissionRepository
    {
        IQueryable<VwUserSystemPermission> GetAllBySqUserSystem(int sqUserSystem);
    }
}