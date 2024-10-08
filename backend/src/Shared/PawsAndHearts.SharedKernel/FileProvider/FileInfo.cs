using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.SharedKernel.FileProvider;

public record FileInfo(FilePath FilePath, string BucketName);