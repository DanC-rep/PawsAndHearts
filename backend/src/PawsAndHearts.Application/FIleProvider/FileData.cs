namespace PawsAndHearts.Application.FIleProvider;

public record UploadFileData(Stream Stream, string BucketName, string ObjectName);

public record DeleteFileData(string FileName, string BucketName);

public record GetFileData(string FileName, string  BucketName);