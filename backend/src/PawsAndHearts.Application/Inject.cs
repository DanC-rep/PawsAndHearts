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
        services.AddScoped<CreateVolunteerHandler>();

        services.AddScoped<UpdateMainInfoHandler>();

        services.AddScoped<UpdateSocialNetworksHandler>();

        services.AddScoped<UpdateRequisitesHandler>();

        services.AddScoped<DeleteVolunteerHandler>();

        services.AddScoped<CreatePetHandler>();

        services.AddScoped<AddPhotosToPetHandler>();

        services.AddScoped<GetVolunteersWithPaginationHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}