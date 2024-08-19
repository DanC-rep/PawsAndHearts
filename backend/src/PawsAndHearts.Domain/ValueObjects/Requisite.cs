using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

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

    public static Result<Requisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("name");

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired("description");

        return new Requisite(name, description);
    }
}