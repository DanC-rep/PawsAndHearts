using PawsAndHearts.Application.Interfaces;
using FileInfo = PawsAndHearts.Application.FIleProvider.FileInfo;

namespace PawsAndHearts.Infrastructure.Files;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IFileProvider _fileProvider;

    public FilesCleanerService(
        IMessageQueue<IEnumerable<FileInfo>> messageQueue, 
        IFileProvider fileProvider)
    {
        _messageQueue = messageQueue;
        _fileProvider = fileProvider;
    }

    public async Task Process(CancellationToken cancellationToken = default)
    {
        var fileInfos = await _messageQueue.ReadAsync(cancellationToken);

        foreach (var fileInfo in fileInfos)
        {
            await _fileProvider.Delete(fileInfo, cancellationToken);
        }
    }
}