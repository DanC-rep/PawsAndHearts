using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

namespace PawsAndHearts.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();

        return services;
    }
}