using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Files;
using PawsAndHearts.Core.Messaging;
using PawsAndHearts.PetManagement.Application;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.PetManagement.Infrastructure.DbContexts;
using PawsAndHearts.PetManagement.Infrastructure.Options;
using PawsAndHearts.PetManagement.Infrastructure.Providers;
using PawsAndHearts.PetManagement.Infrastructure.Repositories;
using PawsAndHearts.SharedKernel.Interfaces;
using FileInfo = PawsAndHearts.SharedKernel.FileProvider.FileInfo;

namespace PawsAndHearts.PetManagement.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddPetManagementInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddMinio(configuration)
            .AddRepositories()
            .AddDatabase();

        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>,
            FilesCleanerMessageQueue<IEnumerable<FileInfo>>>();

        services.AddScoped<IFilesCleanerService, FilesCleanerService>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IPetManagementUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<IPetRepository, PetRepository>();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IVolunteersReadDbContext, ReadDbContext>();

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                               ?? throw new ApplicationException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.AccessKey);
            options.WithSSL(minioOptions.WithSsl);
        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }

}