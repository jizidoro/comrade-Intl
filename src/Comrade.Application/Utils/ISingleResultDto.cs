#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Utils
{
    public interface ISingleResultDto<TDto> : IResultDto
        where TDto : Dto
    {
        TDto Data { get; }
    }
}