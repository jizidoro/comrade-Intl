#region

using System.Threading.Tasks;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore
{
    public interface ISystemUserCreateUseCase
    {
        Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
    }
}