using Microsoft.EntityFrameworkCore;
using Movie.SharedKernel.Domain;

namespace Movie.Infrastructure.Repositories
{
    internal abstract class Repository<T> where T : BaseEntity
    {
        protected readonly MoviesDbContext _context;

        protected Repository(MoviesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
             await _context
                .Set<T>()
                .AddAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context
                .Set<T>()
                .Update(entity);
            return Task.CompletedTask;
        }


        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null)
                return false;

            _context.Set<T>().Remove(entity);
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
