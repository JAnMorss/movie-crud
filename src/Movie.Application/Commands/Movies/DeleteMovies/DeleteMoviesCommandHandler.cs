using Movie.Domain.Interfaces;
using Movie.SharedKernel;
using Movie.SharedKernel.Application.Command;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.Application.Commands.Movies.DeleteMovies;

public sealed class DeleteMoviesCommandHandler : ICommandHandler<DeleteMoviesCommand, Guid>
{
    public readonly IMovieRepository _repository;
    public readonly IUnitOfWork _unitOfWork;

    public DeleteMoviesCommandHandler(IMovieRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteMoviesCommand request, CancellationToken cancellationToken)
    {
        var movie = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (movie is null)
            return Result.Failure<Guid>(new Error("Movie.NotFound", "Movie cannot be found"));

        await _repository.DeleteAsync(movie.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(movie.Id);
    }
}
