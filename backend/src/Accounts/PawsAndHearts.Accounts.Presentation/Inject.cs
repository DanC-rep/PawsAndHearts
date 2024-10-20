using Microsoft.Extensions.DependencyInjection;
using PawsAndHearts.Accounts.Contracts;

namespace PawsAndHearts.Accounts.Presentation;

public static class Inject
{
    public static IServiceCollection AddAAccountsPresentation(this IServiceCollection services)
    {
        services.AddScoped<IAccountsContract, AccountsContract>();

        return services;
    } 
}