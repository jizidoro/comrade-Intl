#region

using AutoMapper;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Application.Utils
{
    public interface IAppService
    {
        IMapper Mapper { get; }
    }
}