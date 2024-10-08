namespace PawsAndHearts.SharedKernel.Interfaces;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken = default);
}