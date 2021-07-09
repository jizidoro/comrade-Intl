#region

using AutoMapper;

#endregion

namespace Comrade.Application.BaseInterfaces
{
    public interface IService
    {
        IMapper Mapper { get; }
    }
}