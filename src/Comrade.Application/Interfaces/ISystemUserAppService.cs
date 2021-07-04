#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.SystemUserDtos;
using Comrade.Application.Filters;
using Comrade.Application.Lookups;
using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Interfaces
{
    public interface ISystemUserAppService : IAppService
    {
        Task<IPageResultDto<SystemUserDto>> GetAll(PaginationFilter paginationFilter = null);
        Task<ListResultDto<LookupDto>> FindByName(string name);
        Task<ISingleResultDto<SystemUserDto>> GetById(int id);
        Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto);
        Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto);
        Task<ISingleResultDto<EntityDto>> Delete(int id);
    }

}