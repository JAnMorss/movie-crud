using Movie.Application.Responses.Movies;
using Movie.SharedKernel.Application.Query;

namespace Movie.Application.Queries.Movies.GetMoviesById;

public sealed record GetMovieByIdQuery(Guid Id) : IQuery<MovieResponse>;
