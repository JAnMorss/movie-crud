using Movie.SharedKernel.Application.Command;

namespace Movie.Application.Commands.Movies.DeleteMovies;

public sealed record DeleteMoviesCommand(Guid Id) : ICommand<Guid>;
