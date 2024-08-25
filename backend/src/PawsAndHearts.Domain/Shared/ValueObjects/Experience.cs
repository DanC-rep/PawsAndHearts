using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record Experience
{
    private Experience(int value)
    {
        Value = value;
    }
    
    public int Value { get; }

    public static Result<Experience, Error> Create(int experience)
    {
        if (experience < 0)
            return Errors.General.ValueIsInvalid("experience");

        return new Experience(experience);
    }
}