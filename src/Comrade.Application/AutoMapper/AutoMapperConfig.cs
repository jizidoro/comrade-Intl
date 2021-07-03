#region

using AutoMapper;
using Comrade.Application.MappingProfiles;

#endregion

namespace Comrade.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new(cfg =>
            {
                cfg.AddProfile(new DomainToDtoMappingProfile());
                cfg.AddProfile(new DtoToDomainMappingProfile());
                cfg.AddProfile(new RequestToDomainProfile());
            });
        }
    }
}