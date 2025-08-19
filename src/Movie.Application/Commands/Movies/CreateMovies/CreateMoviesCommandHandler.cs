using Movie.Domain.Entities;
using Movie.Domain.Interfaces;
using Movie.Domain.ValueObjects;
using Movie.SharedKernel;
using Movie.SharedKernel.Application.Command;
using Movie.SharedKernel.ErrorHandling;
using Movie.SharedKernel.Exceptions;

namespace Movie.Application.Commands.Movies.CreateMovies
{
    public sealed class CreateMoviesCommandHandler : ICommandHandler<CreateMoviesCommand, Guid>
    {
        private readonly IMovieRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMoviesCommandHandler(IMovieRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateMoviesCommand request, CancellationToken cancellationToken)
        {
            try{
                var movie = new MoviesEntity(
                     Guid.NewGuid(),
                     DateTime.Now.ToUniversalTime(),
                     new Title(request.Title),
                     new Description(request.Description),
                     new Category(request.Category)
                );

                await _repository.AddAsync(movie, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return Result.Success(movie.Id);
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(new Error(
                    "Movie.Overlap",
                    "The current booking is overlapping with an existing one")
                );
            }

        }
    }
}
