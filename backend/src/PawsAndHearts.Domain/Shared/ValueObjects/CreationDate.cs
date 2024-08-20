using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record CreationDate
{
    private CreationDate(DateOnly value)
    {
        Value = value;
    }
    
    public DateOnly Value { get; } = default!;

    public static Result<CreationDate, Error> Create(DateOnly creationDate)
    {
        if (creationDate > DateOnly.FromDateTime(DateTime.Now))
            return Errors.General.ValueIsInvalid("creation date");
        
        return new CreationDate(creationDate);
    }
}