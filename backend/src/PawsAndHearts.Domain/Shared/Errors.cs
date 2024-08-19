namespace PawsAndHearts.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null) =>
            Error.Validation("value.is.invalid", $"{name ?? "value"} is invalid");
        
        public static Error ValueIsRequired(string? name = null) =>
            Error.Validation("length.is.invalid", $"invalid {name ?? "value"} length");

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : " for id: " + id;
            return Error.NotFound("record.not.found", $"record not found{forId}");
        }
    }
}