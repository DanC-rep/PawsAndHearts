using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.FIleProvider;

public record FileInfo(FilePath FilePath, string BucketName);