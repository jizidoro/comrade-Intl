#region

using System.Threading.Tasks;

#endregion

namespace Comrade.Core.Helpers.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<int> AffectedRows();
    }
}