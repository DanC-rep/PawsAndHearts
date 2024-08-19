using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record Requisite
{
    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public string Name { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public static Result<Requisite> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Requisite>("Name or description can not be empty");
        }

        return new Requisite(name, description);
    }
}