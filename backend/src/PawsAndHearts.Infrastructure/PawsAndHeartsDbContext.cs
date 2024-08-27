using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Species.Entities;
using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Infrastructure;

public class PawsAndHeartsDbContext(IConfiguration configuration) : DbContext
{
    private new const string Database = "PawsAndHearts";

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

    public DbSet<Breed> Breeds => Set<Breed>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Database));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PawsAndHeartsDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder.AddConsole());
}