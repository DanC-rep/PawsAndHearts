using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.FIleProvider;

public record UploadFileData(Stream Stream, FilePath FilePath, string BucketName);

public record DeleteFileData(string FileName, string BucketName);

public record GetFileData(string FileName, string  BucketName);