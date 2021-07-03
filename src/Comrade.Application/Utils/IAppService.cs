#region

using AutoMapper;

#endregion

namespace Comrade.Application.Utils
{
    public interface IAppService
    {
        IMapper Mapper { get; }
    }
}