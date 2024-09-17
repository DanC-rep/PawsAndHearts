using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.FileProvider;

public record FileInfo(FilePath FilePath, string BucketName);