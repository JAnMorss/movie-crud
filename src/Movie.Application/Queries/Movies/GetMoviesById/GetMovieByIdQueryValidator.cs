using FluentValidation;
using Movie.Domain.Entities;

namespace Movie.Application.Queries.Movies.GetMoviesById;

public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
{
    public GetMovieByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage($"{nameof(MoviesEntity)} cannot be empty");
    }
}
