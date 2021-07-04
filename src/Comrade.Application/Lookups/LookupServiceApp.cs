#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using comrade.Application.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Bases;

#endregion

namespace comrade.Application.Lookups
{
    public class LookupServiceApp<TEntity> : AppService, ILookupServiceApp<TEntity>
        where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;

        public LookupServiceApp(IRepository<TEntity> repository, IMapper mapper)
            : base(mapper)
        {
            _repository = repository;
        }

        public async Task<IList<LookupDto>> GetLookup()
        {
            var list = await Task.Run(() => _repository.GetLookup()
                .ProjectTo<LookupDto>(Mapper.ConfigurationProvider)
                .ToList());

            if (list != null)
            {
                return list.OrderBy(x => x.Value).ToList();
            }

            return new List<LookupDto>();
        }

        public async Task<IList<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate)
        {
            var list = await Task.Run(() => _repository.GetLookup(predicate)
                .ProjectTo<LookupDto>(Mapper.ConfigurationProvider)
                .ToList());

            return list;
        }
    }
}