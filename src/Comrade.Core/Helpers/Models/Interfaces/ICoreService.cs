#region

using System.Threading.Tasks;

#endregion

namespace Comrade.Core.Helpers.Models.Interfaces
{
    public interface ICoreService
    {
        Task<bool> Commit();
    }
}