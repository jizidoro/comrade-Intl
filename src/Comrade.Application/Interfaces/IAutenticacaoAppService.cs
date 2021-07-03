#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos;
using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Interfaces
{
    public interface IAutenticacaoAppService : IAppService
    {
        Task<ISingleResultDto<UsuarioDto>> GerarTokenLoginUsecase(AutenticacaoDto dto);
        Task<ISingleResultDto<EntityDto>> EsquecerSenha(AutenticacaoDto dto);
        Task<ISingleResultDto<EntityDto>> ExpirarSenha(AutenticacaoDto dto);
    }
}