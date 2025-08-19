using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Domain.Interfaces;
using Movie.Infrastructure.Repositories;
using Movie.SharedKernel;

namespace Movie.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<MoviesDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MoviesDbContext>());


        return services;
    }
}
