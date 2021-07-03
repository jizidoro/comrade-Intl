#region

using AutoMapper;
using Comrade.Application.Utils;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Application.Bases
{
    public class AppService
    {
        public AppService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

    }
}