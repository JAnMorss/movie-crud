using Movie.Domain.Entities;
using Movie.Domain.Interfaces;

namespace Movie.Infrastructure.Repositories
{
    internal sealed class MovieRepository : Repository<MoviesEntity>, IMovieRepository
    {
        public MovieRepository(MoviesDbContext context) : base(context)
        {
        }
    }
}
