#region

using System.Threading.Tasks;

#endregion

namespace Comrade.Core.Helpers.Interfaces
{
    public interface IService
    {
        Task<bool> Commit();
    }
}