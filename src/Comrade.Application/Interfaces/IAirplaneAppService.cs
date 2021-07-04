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
        Task<IPageResultDto<AirplaneDto>> GetAll(PaginationFilter paginationFilter = null);
        Task<ISingleResultDto<AirplaneDto>> GetById(int id);
        Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto);
        Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto);
        Task<ISingleResultDto<EntityDto>> Delete(int id);
    }
}