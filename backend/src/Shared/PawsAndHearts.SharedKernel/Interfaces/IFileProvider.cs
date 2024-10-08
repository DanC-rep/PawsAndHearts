using CSharpFunctionalExtensions;
using PawsAndHearts.SharedKernel.FileProvider;
using PawsAndHearts.SharedKernel.ValueObjects;
using FileInfo = PawsAndHearts.SharedKernel.FileProvider.FileInfo;

namespace PawsAndHearts.SharedKernel.Interfaces;

public interface IFileProvider
{
    Task<Result<FilePathList, Error>> UploadFiles(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFiles(
        IEnumerable<FileInfo> fileInfos,
        CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> Delete(
        FileInfo fileInfo, 
        CancellationToken cancellationToken = default);
}