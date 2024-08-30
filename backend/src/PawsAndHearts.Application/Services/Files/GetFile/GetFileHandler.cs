using CSharpFunctionalExtensions;
using PawsAndHearts.Application.FIleProvider;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Services.Files.GetFile;

public class GetFileHandler
{
    private readonly IFileProvider _fileProvider;

    public GetFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        GetFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var getFileData = new GetFileData(request.FileName, request.BucketName);

        var result = await _fileProvider.GetFile(getFileData, cancellationToken);

        return result;
    }
}