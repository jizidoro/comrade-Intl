#region

using Comrade.External.Bases;

#endregion

namespace Comrade.External.Utils
{
    public interface ISingleResultDto<TDto>
        where TDto : Dto
    {
        TDto Data { get; }
    }
}