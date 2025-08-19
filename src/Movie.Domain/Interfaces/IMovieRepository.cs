using Movie.Domain.Entities;

namespace Movie.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MoviesEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<MoviesEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(MoviesEntity movies, CancellationToken cancellationToken = default);

        Task UpdateAsync(MoviesEntity movies, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
