﻿#region

using AutoMapper;

#endregion

namespace Comrade.Application.Bases
{
    public class Service
    {
        public Service(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }
    }
}