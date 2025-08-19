using Movie.Domain.ValueObjects;
using Movie.SharedKernel.Domain;

namespace Movie.Domain.Entities;

public class MoviesEntity : BaseEntity
{
    private MoviesEntity() { }

    public MoviesEntity(
        Guid id,
        DateTime createDate,
        Title title,
        Description description,
        Category category) : base(id)
    {
        Title = title;
        Description = description;
        Category = category;
    }

    public Title Title { get; private set; }

    public Description Description { get; private set; }

    public Category Category { get; private set; }

    public void UpdateDetails(
        Title title,
        Description description, 
        Category category)
    {
        Title = title;
        Description = description;
        Category = category;
    }

}
