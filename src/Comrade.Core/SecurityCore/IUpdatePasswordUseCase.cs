#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SecurityCore
{
    public interface IUpdatePasswordUseCase
    {
        Task<ISingleResult<SystemUser>> Execute(SystemUser entity);
    }
}