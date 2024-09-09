using CSharpFunctionalExtensions;
using PawsAndHearts.Application.FIleProvider;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Interfaces;

public interface IFileProvider
{
    Task<Result<FilePathList, Error>> UploadFiles(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default);
}