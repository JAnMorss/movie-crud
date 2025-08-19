using Movie.Application.DTOs.Movies;
using Movie.SharedKernel.Application.Command;

namespace Movie.Application.Commands.Movies.UpdateMovies;

public sealed record UpdateMoviesCommand(
    Guid Id,
    string Title,
    string Description,
    string Category) : ICommand<MovieResponse>;
