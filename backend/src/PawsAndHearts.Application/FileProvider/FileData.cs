namespace PawsAndHearts.Application.FileProvider;

public record UploadFileData(Stream Stream, FileInfo Info);

public record GetFileData(string FileName, string  BucketName);