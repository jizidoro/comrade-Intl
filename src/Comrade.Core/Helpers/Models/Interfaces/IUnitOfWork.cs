#region

using System.Threading.Tasks;

#endregion

namespace Comrade.Core.Helpers.Models.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<int> AffectedRows();
    }
}