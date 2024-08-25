using Microsoft.EntityFrameworkCore;
using PawsAndHearts.Infrastructure;

namespace PawsAndHearts.API.Extensions;

public static class AppExtensions
{
    public static async Task ApplyMigration(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PawsAndHeartsDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}