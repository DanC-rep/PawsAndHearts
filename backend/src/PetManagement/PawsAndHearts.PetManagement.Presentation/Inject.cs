using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.PetManagement.Contracts;

namespace PawsAndHearts.PetManagement.Presentation;

public static class Inject
{
    public static IServiceCollection AddPetManagementPresentation(this IServiceCollection services)
    {
        services.AddScoped<IPetManagementContract, PetManagementContract>();

        return services;
    }
}