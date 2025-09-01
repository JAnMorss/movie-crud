using Movie.Domain.Entities;

namespace Movie.Application.Responses.Movies;

public sealed class MovieResponse
{
    public Guid Id { get; init; }
    public DateTime CreateDate { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Category { get; init; }

    public static MovieResponse FromEntity(MoviesEntity movies)
    {
        return new MovieResponse
        {
            Id = movies.Id,
            CreateDate = movies.CreateDate,
            Title = movies.Title.Value,
            Description = movies.Description.Value,
            Category = movies.Category.Value
        };
    }
}
