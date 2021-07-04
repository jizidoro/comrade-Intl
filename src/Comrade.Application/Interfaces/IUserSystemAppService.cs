#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UserSystemDtos;
using comrade.Application.Filters;
using comrade.Application.Lookups;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Interfaces
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