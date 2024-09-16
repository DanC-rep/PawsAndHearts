using CSharpFunctionalExtensions;
using PawsAndHearts.Application.FIleProvider;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using FileInfo = PawsAndHearts.Application.FIleProvider.FileInfo;

namespace PawsAndHearts.Application.Interfaces;

public interface IFileProvider
{
    Task<Result<FilePathList, Error>> UploadFiles(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> Delete(
        FileInfo fileInfo, 
        CancellationToken cancellationToken = default);
}