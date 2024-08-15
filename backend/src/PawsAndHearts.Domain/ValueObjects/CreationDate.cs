using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record CreationDate
{
    private CreationDate(DateOnly value)
    {
        Value = value;
    }
    
    public DateOnly Value { get; } = default!;

    public Result<CreationDate> Create(DateOnly birthDate)
    {
        return new CreationDate(birthDate);
    }
}