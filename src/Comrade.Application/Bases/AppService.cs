﻿#region

using AutoMapper;

#endregion

namespace comrade.Application.Bases
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