#region

using System.Collections.Generic;

#endregion

namespace Comrade.Application.Bases.Interfaces
{
    public interface IListResultDto<TDto> : IResultDto
        where TDto : Dto
    {
        IList<TDto>? Data { get; set; }
    }
}