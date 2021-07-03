#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.UserSystemDtos;
using Comrade.Application.Filters;
using Comrade.Application.Lookups;
using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Interfaces
{
    public interface IUserSystemAppService : IAppService
    {
        Task<IPageResultDto<UserSystemDto>> GetAll(PaginationFilter paginationFilter = null);
        Task<ListResultDto<LookupDto>> FindByName(string name);
        Task<ISingleResultDto<UserSystemDto>> GetById(int id);
        Task<ISingleResultDto<EntityDto>> Create(UserSystemCreateDto dto);
        Task<ISingleResultDto<EntityDto>> Edit(UserSystemEditDto dto);
        Task<ISingleResultDto<EntityDto>> Delete(int id);
    }
}