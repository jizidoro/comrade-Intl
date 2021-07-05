#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;

#endregion

namespace Comrade.Infrastructure.DataAccess
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ComradeContext _context;
        private bool _disposed;

        public UnitOfWork(ComradeContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
            {
                this._context.Dispose();
            }

            this._disposed = true;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        /// <inheritdoc />
        public async Task<int> Save()
        {
            int affectedRows = await this._context
                .SaveChangesAsync()
                .ConfigureAwait(false);
            return affectedRows;
        }
    }
}