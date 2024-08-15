using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record BirthDate
{
    private BirthDate(DateOnly value)
    {
        Value = value;
    }
    
    public DateOnly Value { get; } = default!;

    public Result<BirthDate> Create(DateOnly birthDate)
    {
        return new BirthDate(birthDate);
    }
}