namespace PawsAndHearts.Application.FIleProvider;

public record UploadFileData(Stream Stream, FileInfo Info);

public record DeleteFileData(string FileName, string BucketName);

public record GetFileData(string FileName, string  BucketName);