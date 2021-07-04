#region

using AutoMapper;

#endregion

namespace Comrade.Application.BaseInterfaces
{
    public interface IAppService
    {
        IMapper Mapper { get; }
    }
}