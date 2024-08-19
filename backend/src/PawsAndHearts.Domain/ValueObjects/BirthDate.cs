using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.ValueObjects;

public record BirthDate
{
    private BirthDate(DateOnly value)
    {
        Value = value;
    }
    
    public DateOnly Value { get; } = default!;

    public static Result<BirthDate, Error> Create(DateOnly birthDate)
    {
        return new BirthDate(birthDate);
    }
}