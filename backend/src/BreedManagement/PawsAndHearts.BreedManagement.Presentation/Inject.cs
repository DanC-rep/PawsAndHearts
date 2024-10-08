using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.BreedManagement.Contracts;

namespace PawsAndHearts.BreedManagement.Presentation;

public static class Inject
{
   public static IServiceCollection AddBreedManagementPresentation(this IServiceCollection services)
   {
      services.AddScoped<IBreedManagementContract, BreedManagementContract>();

      return services;
   } 
}