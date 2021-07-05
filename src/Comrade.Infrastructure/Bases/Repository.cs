#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Comrade.Infrastructure.Bases
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly ComradeContext _db;
        private readonly DbSet<TEntity> _dbSet;
        private bool _disposed;

        public Repository(ComradeContext context)
        {
            _db = context;
            _dbSet = _db.Set<TEntity>();
        }

        public virtual async Task Add(TEntity obj)
        {
            await _dbSet.AddAsync(obj).ConfigureAwait(false);
        }

        public virtual void Update(TEntity obj)
        {
            _dbSet.Update(obj);
        }

        public virtual void Remove(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public virtual async Task<TEntity?> GetById(int id)
        {
            return await GetById(id, null, includes: null).ConfigureAwait(false);
        }

        public virtual async Task<TEntity?> GetById(int id, params string[] includes)
        {
            return await GetById(id, null, includes).ConfigureAwait(false);
        }

        public virtual async Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>> projection)
        {
            return await GetById(id, projection, null).ConfigureAwait(false);
        }

        public virtual async Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>>? projection,
            params string[]? includes)
        {
            var query = GetAll();
            if (projection != null) query = query.Select(projection);

            if (includes != null && includes.Length > 0)
                foreach (var include in includes)
                    query = query.Include(include);

            query = query.Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByValue(string value)
        {
            return await GetByValue(value, null).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByValue(string value, Expression<Func<TEntity, TEntity>>? projection)
        {
            var query = GetAll();
            if (projection != null) query = query.Select(projection);

            query = query.Where(p => p.Value == value);

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> ValueExists(int id, string value)
        {
            var exists = await GetAll()
                .Where(p => p.Id != id
                            && p.Value == value)
                .AnyAsync().ConfigureAwait(false);

            return exists;
        }

        public virtual async Task<bool> ChildrenExists(int id, Expression<Func<TEntity, bool>> predicate,
            string? include = null)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(include)) query = query.Include(include);

            var exists = await query
                .Where(p => p.Id == id)
                .Where(predicate)
                .AnyAsync().ConfigureAwait(false);

            return exists;
        }

        public virtual Task<bool> GetAllChildren(int id, Expression<Func<TEntity, bool>> predicate,
            string? include = null)
        {
            var query = GetAll();

            if (!string.IsNullOrEmpty(include)) query = query.Include(include);

            var allChild = query
                .Where(p => p.Id == id)
                .Where(predicate).AnyAsync();

            return allChild;
        }

        public virtual async Task<bool> IsUnique(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetAll();

            var exists = await query
                .Where(predicate)
                .AnyAsync().ConfigureAwait(false);

            return !exists;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }


        public virtual IQueryable<TEntity> GetAllAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, TEntity>> projection)
        {
            return _dbSet.AsNoTracking().Select(projection);
        }

        public async Task<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public virtual IQueryable<LookupEntity> GetLookup()
        {
            return _dbSet
                .Take(100)
                .Select(s => new LookupEntity {Key = s.Key, Value = s.Value});
        }

        public IQueryable<LookupEntity> GetLookup(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet
                .AsNoTracking()
                .Take(100)
                .Where(predicate)
                .Select(s => new LookupEntity {Key = s.Key, Value = s.Value});
        }


        public virtual IQueryable<TEntity> GetLookupQuery(Expression<Func<TEntity, TEntity>> projection)
        {
            var query = GetAll();

            query = query.Select(projection);

            return query;
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _db.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}