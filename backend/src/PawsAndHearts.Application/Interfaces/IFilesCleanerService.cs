namespace PawsAndHearts.Application.Interfaces;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken = default);
}