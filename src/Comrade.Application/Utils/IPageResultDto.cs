#region

using System.Collections.Generic;
using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Utils
{
    public interface IPageResultDto<TDto> : IResultDto
        where TDto : Dto
    {
        IList<TDto> Data { get; set; }
    }
}