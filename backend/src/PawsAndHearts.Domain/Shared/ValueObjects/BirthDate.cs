using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record BirthDate
{
    private BirthDate(DateOnly value)
    {
        Value = value;
    }
    
    public DateOnly Value { get; } = default!;

    public static Result<BirthDate, Error> Create(DateOnly birthDate)
    {
        if (birthDate > DateOnly.FromDateTime(DateTime.Now) || birthDate.Year < 1900)
            return Errors.General.ValueIsInvalid("birth date");
        
        return new BirthDate(birthDate);
    }
}