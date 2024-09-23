using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;
using PawsAndHearts.Application.VolunteerManagement.UseCases.AddPhotosToPet;
using PawsAndHearts.Application.VolunteerManagement.UseCases.CreatePet;
using PawsAndHearts.Application.VolunteerManagement.UseCases.CreateVolunteer;
using PawsAndHearts.Application.VolunteerManagement.UseCases.DeleteVolunteer;
using PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateMainInfo;
using PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateRequisites;
using PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateSocialNetworks;

namespace PawsAndHearts.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryHandlerWithResult<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}