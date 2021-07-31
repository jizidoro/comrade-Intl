#region

using System.Threading.Tasks;

#endregion

namespace Comrade.Core.Bases.Interfaces
{
    public interface ICoreService
    {
        Task<bool> Commit();
    }
}