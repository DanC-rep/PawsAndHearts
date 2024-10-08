using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PawsAndHearts.SharedKernel.Interfaces;

namespace PawsAndHearts.Framework.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public FilesCleanerBackgroundService(
        ILogger<FilesCleanerBackgroundService> logger, 
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Files Cleaner Background Service is started");
        
        await using var scope = _scopeFactory.CreateAsyncScope();

        var fileCleanerService = scope.ServiceProvider.GetRequiredService<IFilesCleanerService>();
        
        while (!cancellationToken.IsCancellationRequested)
        {
            await fileCleanerService.Process(cancellationToken);
        }

        await Task.CompletedTask;
    }
}