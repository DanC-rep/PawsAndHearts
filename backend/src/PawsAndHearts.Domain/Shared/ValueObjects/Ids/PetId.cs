using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects.Ids;

public class PetId : ValueObject
{
    private PetId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static PetId NewId() => new(Guid.NewGuid());

    public static PetId Empty() => new(Guid.Empty);

    public static PetId Create(Guid id) => new(id);

    public static implicit operator PetId(Guid id) => new(id);

    public static implicit operator Guid(PetId petId)
    {
        ArgumentNullException.ThrowIfNull(petId);

        return petId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}