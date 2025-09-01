using Movie.Application.Responses.Movies;
using Movie.Domain.Interfaces;
using Movie.SharedKernel.Application.Query;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.Application.Queries.Movies.GetMovies;

public sealed class GetMoviesQueryHandler : IQueryHandler<GetMoviesQuery, IEnumerable<MovieResponse>>
{
    public readonly IMovieRepository _repository;

    public GetMoviesQueryHandler(IMovieRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<MovieResponse>>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _repository.GetAllAsync(cancellationToken);
        if (movies is null)
            return Result.Failure<IEnumerable<MovieResponse>>(
                new Error(
                    "Movie.NotFound", 
                    "Movie cannot be found"
                ));

        IEnumerable<MovieResponse> result = movies.Select(MovieResponse.FromEntity);

        return Result.Success(result);
    }
}
