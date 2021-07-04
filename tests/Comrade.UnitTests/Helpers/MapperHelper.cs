#region

using AutoMapper;
using comrade.Application.AutoMapper;
using comrade.Application.MappingProfiles;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public class MapperHelper
    {
        public static IMapper ConfigMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToDomainMappingProfile());
                cfg.AddProfile(new DomainToDtoMappingProfile());
                cfg.AddProfile(new RequestToDomainProfile());
            }).CreateMapper();
        }
    }
}