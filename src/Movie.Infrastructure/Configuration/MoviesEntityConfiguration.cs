using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities;

namespace Movie.Infrastructure.Configuration;

public class MoviesEntityConfiguration : IEntityTypeConfiguration<MoviesEntity>
{
    public void Configure(EntityTypeBuilder<MoviesEntity> builder)
    {
        builder.OwnsOne(m => m.Title, t =>
        {
            t.Property(p => p.Value).HasColumnName("Title").IsRequired();
        });

        builder.OwnsOne(m => m.Description, d =>
        {
            d.Property(p => p.Value).HasColumnName("Description").IsRequired();
        });

        builder.OwnsOne(m => m.Category, c =>
        {
            c.Property(p => p.Value).HasColumnName("Category").IsRequired();
        });
    }
}

