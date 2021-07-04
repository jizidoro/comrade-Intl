#region

using System.Collections.Generic;
using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Utils
{
    public interface IListResultDto<TDto> : IResultDto
        where TDto : Dto
    {
        IList<TDto> Data { get; set; }
    }
}