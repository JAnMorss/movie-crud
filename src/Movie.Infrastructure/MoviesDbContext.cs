using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities;
using Movie.SharedKernel;
using Movie.SharedKernel.Domain;

namespace Movie.Infrastructure;

public class MoviesDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public MoviesDbContext(DbContextOptions<MoviesDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<MoviesEntity> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEventsAsync();
        return result;
    }
    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<BaseEntity>()
            .SelectMany(e => e.Entity.GetDomainEvents())
            .ToList();

        if (!domainEvents.Any()) return;

        foreach (var entity in ChangeTracker.Entries<BaseEntity>())
        {
            entity.Entity.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }

}
