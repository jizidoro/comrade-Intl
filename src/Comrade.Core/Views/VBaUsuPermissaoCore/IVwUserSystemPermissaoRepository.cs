#region

using System.Linq;
using Comrade.Domain.Models.Views;

#endregion

namespace Comrade.Core.Views.VBaUsuPermissaoCore
{
    public interface IVwUserSystemPermissionRepository
    {
        IQueryable<VwUserSystemPermission> GetAllBySqUserSystem(int sqUserSystem);
    }
}