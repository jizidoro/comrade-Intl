#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos.UsuarioSistemaDtos;
using Comrade.Application.Filters;
using Comrade.Application.Utils;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Application.Interfaces
{
    public interface IUsuarioSistemaAppService : IAppService
    {
        Task<IPageResultDto<UsuarioSistemaDto>> Listar(PaginationFilter paginationFilter = null);
        Task<ListResultDto<LookupDto>> BuscarPorNome(string nome);
        Task<ISingleResultDto<UsuarioSistemaDto>> Obter(int id);
        Task<ISingleResultDto<EntityDto>> Incluir(UsuarioSistemaIncluirDto dto);
        Task<ISingleResultDto<EntityDto>> Editar(UsuarioSistemaEditarDto dto);
        Task<ISingleResultDto<EntityDto>> Excluir(int id);
    }
}