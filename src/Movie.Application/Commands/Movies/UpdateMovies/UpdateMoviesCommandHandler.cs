
using MediatR;
using Movie.Application.DTOs.Movies;
using Movie.Domain.Entities;
using Movie.Domain.Interfaces;
using Movie.Domain.ValueObjects;
using Movie.SharedKernel;
using Movie.SharedKernel.Application.Command;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.Application.Commands.Movies.UpdateMovies
{
    public sealed class UpdateMoviesCommandHandler : ICommandHandler<UpdateMoviesCommand, MovieResponse>
    {
        public readonly IMovieRepository _repository;
        public readonly IUnitOfWork _unitOfWork;

        public UpdateMoviesCommandHandler(IMovieRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MovieResponse>> Handle(UpdateMoviesCommand request, CancellationToken cancellationToken)
        {
            var existingMovie = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (existingMovie is null)
                return Result.Failure<MovieResponse>(new Error("Movie.NotFound", "Movie cannot be found"));

            existingMovie.UpdateDetails(
                new Title(request.Title),
                new Description(request.Description),
                new Category(request.Category)
            );

            await _repository.UpdateAsync(existingMovie, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = MovieResponse.FromEntity(existingMovie);

            return Result.Success(result);
        } 
    }
}
