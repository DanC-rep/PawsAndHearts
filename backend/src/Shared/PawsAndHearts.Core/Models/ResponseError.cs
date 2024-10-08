namespace PawsAndHearts.Core.Models;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);