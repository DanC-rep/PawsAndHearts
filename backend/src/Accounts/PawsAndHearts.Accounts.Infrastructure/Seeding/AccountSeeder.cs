using Microsoft.Extensions.DependencyInjection;

namespace PawsAndHearts.Accounts.Infrastructure.Seeding;

public class AccountSeeder(IServiceScopeFactory serviceScopeFactory)
{
    public async Task SeedAsync()
    {
        using var scope = serviceScopeFactory.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<AccountsSeederService>();

        await service.SeedAsync();
    }
}