#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Interfaces
{
    public interface IAthenticationAppService : IAppService
    {
        Task<ISingleResultDto<UserDto>> GenerateTokenLoginUseCase(AthenticationDto dto);
        Task<ISingleResultDto<EntityDto>> ForgotPassword(AthenticationDto dto);
        Task<ISingleResultDto<EntityDto>> ExpirarPassword(AthenticationDto dto);
    }
}