#region

using System.Threading.Tasks;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore
{
    public interface ISystemUserEditUseCase
    {
        Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
    }
}