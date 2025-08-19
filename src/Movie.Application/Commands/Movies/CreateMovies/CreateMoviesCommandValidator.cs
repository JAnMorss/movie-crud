using FluentValidation;
using Movie.Domain.Entities;

namespace Movie.Application.Commands.Movies.CreateMovies;

public class CreateMoviesCommandValidator : AbstractValidator<CreateMoviesCommand>
{
    public CreateMoviesCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage($"{nameof(MoviesEntity.Title)} cannot be empty")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage($"{nameof(MoviesEntity.Description)} cannot be empty")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage($"{nameof(MoviesEntity.Category)} cannot be empty")
            .MaximumLength(30).WithMessage("Category cannot exceed 30 characters.");
    }
}