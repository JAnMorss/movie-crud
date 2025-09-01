using Movie.Application.Responses.Movies;
using Movie.SharedKernel.Application.Command;

namespace Movie.Application.Commands.Movies.CreateMovies;

public sealed record CreateMoviesCommand(
    string Title, 
    string Description,
    string Category) : ICommand<Guid>;
