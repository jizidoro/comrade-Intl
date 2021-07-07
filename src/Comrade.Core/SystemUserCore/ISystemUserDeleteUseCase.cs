#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Models.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore
{
    public interface ISystemUserDeleteUseCase
    {
        Task<ISingleResult<SystemUser>> Execute(int id);
    }
}