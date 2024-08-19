using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Infrastructure.Repositories;

namespace PawsAndHearts.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<PawsAndHeartsDbContext>();

        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        return services;
    }
}