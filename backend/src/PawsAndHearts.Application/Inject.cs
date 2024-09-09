using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Application.Services.Volunteers.AddPhotosToPet;
using PawsAndHearts.Application.Services.Volunteers.CreatePet;
using PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;
using PawsAndHearts.Application.Services.Volunteers.DeleteVolunteer;
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

        services.AddScoped<CreatePetHandler>();

        services.AddScoped<AddPhotosToPetHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}