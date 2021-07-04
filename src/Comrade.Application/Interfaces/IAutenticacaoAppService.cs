#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos;
using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Interfaces
{
    public interface IAuthenticationAppService : IAppService
    {
        Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
        Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto);
        Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto);
    }
}