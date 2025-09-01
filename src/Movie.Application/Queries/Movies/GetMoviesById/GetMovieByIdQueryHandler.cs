using Movie.Application.Responses.Movies;
using Movie.Domain.Interfaces;
using Movie.SharedKernel.Application.Query;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.Application.Queries.Movies.GetMoviesById
{
    public sealed class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, MovieResponse>
    {
        public readonly IMovieRepository _repository;

        public GetMovieByIdQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<MovieResponse>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (movie is null)
                return Result.Failure<MovieResponse>(
                    new Error(
                        "Movie.NotFound",
                        "Movie cannot be found"
                    ));


            return MovieResponse.FromEntity(movie);
        }
    }
}
