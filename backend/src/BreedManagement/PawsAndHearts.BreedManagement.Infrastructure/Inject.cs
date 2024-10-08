using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.BreedManagement.Infrastructure.DbContexts;
using PawsAndHearts.BreedManagement.Infrastructure.Repositories;
using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddBreedManagementInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddRepositories()
            .AddDatabase();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IBreedManagementUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<ISpeciesReadDbContext, ReadDbContext>();

        return services;
    }
}