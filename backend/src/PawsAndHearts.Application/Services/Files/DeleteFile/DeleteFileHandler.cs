using CSharpFunctionalExtensions;
using PawsAndHearts.Application.FIleProvider;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Services.Files.DeleteFile;

public class DeleteFileHandler
{
    private readonly IFileProvider _fileProvider;

    public DeleteFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        DeleteFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var deleteFileData = new DeleteFileData(request.FileName, request.BucketName);

        var result = await _fileProvider.DeleteFile(deleteFileData, cancellationToken);

        return result;
    }
}