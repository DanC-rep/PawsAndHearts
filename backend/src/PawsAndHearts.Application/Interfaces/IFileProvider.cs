using CSharpFunctionalExtensions;
using PawsAndHearts.Application.FIleProvider;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Interfaces;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(
        UploadFileData uploadFileData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeleteFile(
        DeleteFileData deleteFileData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetFile(
        GetFileData getFileData,
        CancellationToken cancellationToken = default);
}