using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;
using PawsAndHearts.Application.Services.Volunteers.Delete;
using PawsAndHearts.Application.Services.Volunteers.UpdateMainInfo;
using PawsAndHearts.Application.Services.Volunteers.UpdateRequisites;
using PawsAndHearts.Application.Services.Volunteers.UpdateSocialNetworks;

namespace PawsAndHearts.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();

        services.AddScoped<UpdateMainInfoHandler>();

        services.AddScoped<UpdateSocialNetworksHandler>();

        services.AddScoped<UpdateRequisitesHandler>();

        services.AddScoped<DeleteVolunteerHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}