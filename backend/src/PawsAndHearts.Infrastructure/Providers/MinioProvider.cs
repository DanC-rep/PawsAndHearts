using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PawsAndHearts.Application.FIleProvider;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    
    public async Task<Result<string, Error>> UploadFile(
        UploadFileData uploadFileData, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(uploadFileData.BucketName);
        
            var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (!bucketExists)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(uploadFileData.BucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(uploadFileData.BucketName)
                .WithStreamData(uploadFileData.Stream)
                .WithObjectSize(uploadFileData.Stream.Length)
                .WithObject(uploadFileData.ObjectName);

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            
            _logger.LogInformation("File {fileName} was upload successfully", result.ObjectName);

            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio");
            
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }

    public async Task<Result<string, Error>> DeleteFile(
        DeleteFileData deleteFileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(deleteFileData.BucketName);
            
            var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (!bucketExists)
            {
                _logger.LogWarning("Bucket does not exists in minio");

                return Error.Failure("bucket.not.exists", "Bucket does not exists in minio");
            }

            var objectExistsArgs = new StatObjectArgs()
                .WithBucket(deleteFileData.BucketName)
                .WithObject(deleteFileData.FileName);

            await _minioClient.StatObjectAsync(objectExistsArgs, cancellationToken);

            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(deleteFileData.BucketName)
                .WithObject(deleteFileData.FileName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
            
            _logger.LogInformation("File {fileName} was deleted successfully", deleteFileData.FileName);

            return deleteFileData.FileName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to delete file in minio");
            
            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }

    public async Task<Result<string, Error>> GetFile(
        GetFileData getFileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(getFileData.BucketName);
            
            var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (!bucketExists)
            {
                _logger.LogWarning("Bucket does not exists in minio");

                return Error.Failure("bucket.not.exists", "Bucket does not exists in minio");
            }

            var objectExistsArgs = new StatObjectArgs()
                .WithBucket(getFileData.BucketName)
                .WithObject(getFileData.FileName);

            await _minioClient.StatObjectAsync(objectExistsArgs, cancellationToken);

            var getObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(getFileData.BucketName)
                .WithObject(getFileData.FileName)
                .WithExpiry(60 * 60 * 24);

            var result = await _minioClient.PresignedGetObjectAsync(getObjectArgs);
            
            _logger.LogInformation("File {fileName} was received successfully", getFileData.FileName);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to get file in minio");
            return Error.Failure("file.get", "Fail to get file in minio");
        }
    }
}