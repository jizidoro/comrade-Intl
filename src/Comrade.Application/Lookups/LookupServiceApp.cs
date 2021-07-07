#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Comrade.Application.Bases;
using Comrade.Core.Helpers.Models.Interfaces;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Application.Lookups
{
    public class LookupServiceApp<TEntity> : Service, ILookupServiceApp<TEntity>
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
                .ToList()).ConfigureAwait(false);

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
                .ToList()).ConfigureAwait(false);

            return list;
        }
    }
}