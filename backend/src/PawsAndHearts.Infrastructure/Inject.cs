using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Infrastructure.BackgroundServices;
using PawsAndHearts.Infrastructure.Files;
using PawsAndHearts.Infrastructure.MessageQueues;
using PawsAndHearts.Infrastructure.Options;
using PawsAndHearts.Infrastructure.Providers;
using PawsAndHearts.Infrastructure.Repositories;
using FileInfo = PawsAndHearts.Application.FIleProvider.FileInfo;

namespace PawsAndHearts.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<PawsAndHeartsDbContext>();

        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddMinio(configuration);

        services.AddHostedService<FilesCleanerBackgroundService>();

        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>,
            FilesCleanerMessageQueue<IEnumerable<FileInfo>>>();

        services.AddScoped<IFilesCleanerService, FilesCleanerService>();

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