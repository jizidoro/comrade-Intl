#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.AirplaneDtos;
using Comrade.Application.Filters;
using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Interfaces
{
    public interface IAirplaneAppService : IAppService
    {
        Task<IPageResultDto<AirplaneDto>> Listar(PaginationFilter paginationFilter = null);
        Task<ISingleResultDto<AirplaneDto>> Obter(int id);
        Task<ISingleResultDto<EntityDto>> Incluir(AirplaneIncluirDto dto);
        Task<ISingleResultDto<EntityDto>> Editar(AirplaneEditarDto dto);
        Task<ISingleResultDto<EntityDto>> Excluir(int id);
    }
}