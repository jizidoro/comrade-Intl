#region

using AutoMapper;
using Comrade.Application.Filters;
using Comrade.Application.Queries;

#endregion

namespace Comrade.Application.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}