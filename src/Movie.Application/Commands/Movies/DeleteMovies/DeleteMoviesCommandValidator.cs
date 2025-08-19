using FluentValidation;
using Movie.Domain.Entities;

namespace Movie.Application.Commands.Movies.DeleteMovies
{
    public class DeleteMoviesCommandValidator : AbstractValidator<DeleteMoviesCommand>
    {
        public DeleteMoviesCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage($"{nameof(MoviesEntity.Id)} cannot be empty");
        }
    }
}
