using Movie.Application.Responses.Movies;
using Movie.SharedKernel.Application.Query;

namespace Movie.Application.Queries.Movies.GetMovies;

public sealed record GetMoviesQuery() : IQuery<IEnumerable<MovieResponse>>;
