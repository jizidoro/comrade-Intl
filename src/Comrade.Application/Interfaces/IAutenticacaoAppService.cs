#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Interfaces
{
    public interface IAuthenticationAppService : IAppService
    {
        Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
        Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto);
        Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto);
    }
}