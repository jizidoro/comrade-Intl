#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Filters;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Interfaces
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