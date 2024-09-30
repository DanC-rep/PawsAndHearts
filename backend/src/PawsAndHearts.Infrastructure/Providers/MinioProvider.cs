using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PawsAndHearts.Application.FileProvider;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using FileInfo = PawsAndHearts.Application.FileProvider.FileInfo;

namespace PawsAndHearts.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    public const int MAX_DEGREE_OF_PARALLELISM = 10;
    
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    
    public async Task<Result<FilePathList, Error>> UploadFiles(
        IEnumerable<UploadFileData> filesData, 
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = filesData.ToList();

        try
        {
            await IfBucketNotExistsCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file => 
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);

            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            return (FilePathList)results;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio");

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }

    public async Task<UnitResult<Error>> DeleteFiles(
        IEnumerable<FileInfo> fileInfos,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = fileInfos.ToList();

        try
        {
            var bucketResult = await IfBucketNotExistsReturnError(filesList, cancellationToken);

            if (bucketResult.IsFailure)
                return bucketResult.Error;

            var tasks = filesList.Select(async file => 
                await DeleteObject(file, semaphoreSlim, cancellationToken));

            var deleteResult = await Task.WhenAll(tasks);

            if (deleteResult.Any(p => p.IsFailure))
                return deleteResult.First().Error;

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to delete file in minio");

            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }

    public async Task<UnitResult<Error>> Delete(
        FileInfo fileInfo, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(fileInfo.BucketName)
                .WithObject(fileInfo.FilePath.Path);

            await _minioClient.RemoveObjectAsync(args, cancellationToken);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to delete file in minio with {path} in bucket {bucket}",
                fileInfo.FilePath.Path,
                fileInfo.BucketName);
            
            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }

    private async Task<Result<FilePath, Error>> PutObject(
        UploadFileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken = default)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.Info.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.Info.FilePath.Path);

        try
        {
            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.Info.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with {path} in bucket {bucket}",
                fileData.Info.FilePath.Path,
                fileData.Info.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
    
    private async Task<UnitResult<Error>> DeleteObject(
        FileInfo fileInfo,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken = default)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var deleteObjectArgs = new RemoveObjectArgs()
            .WithBucket(fileInfo.BucketName)
            .WithObject(fileInfo.FilePath.Path);

        try
        {
            await _minioClient.RemoveObjectAsync(deleteObjectArgs, cancellationToken);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to delete file in minio with {path} in bucket {bucket}",
                fileInfo.FilePath.Path,
                fileInfo.BucketName);

            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketNotExistsCreateBucket(
        IEnumerable<UploadFileData> filesData,
        CancellationToken cancellationToken = default)
    {
        HashSet<string> bucketNames = [..filesData.Select(file => file.Info.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExists = await _minioClient
                .BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (!bucketExists)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    private async Task<UnitResult<Error>> IfBucketNotExistsReturnError(
        IEnumerable<FileInfo> fileInfos,
        CancellationToken cancellationToken = default)
    {
        HashSet<string> bucketNames = [..fileInfos.Select(file => file.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExists = await _minioClient
                .BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (!bucketExists)
                return Error.NotFound("bucket.exists", "Bucket does not exist");
        }

        return Result.Success<Error>();
    }
}