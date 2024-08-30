namespace PawsAndHearts.Application.Services.Files.UploadFile;

public record UploadFileRequest(Stream Stream, string BucketName, string FileName);