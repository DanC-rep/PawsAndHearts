using CSharpFunctionalExtensions;
using PawsAndHearts.Application.FIleProvider;
using PawsAndHearts.Domain.Shared;
using IFileProvider = PawsAndHearts.Application.Interfaces.IFileProvider;

namespace PawsAndHearts.Application.Services.Files.UploadFile;

public class UploadFileHandler
{
    private readonly IFileProvider _fileProvider;

    public UploadFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        UploadFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var uploadFileData = new UploadFileData(request.Stream, request.BucketName, request.FileName);
        
        var result = await _fileProvider.UploadFile(uploadFileData, cancellationToken);

        return result;
    }
}