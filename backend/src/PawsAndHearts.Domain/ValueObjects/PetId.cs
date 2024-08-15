using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static Result<PetId> NewId() => new PetId(Guid.NewGuid());

    public static Result<PetId> Empty() => new PetId(Guid.Empty);

    public static Result<PetId> Create(Guid id) => new PetId(id);
}