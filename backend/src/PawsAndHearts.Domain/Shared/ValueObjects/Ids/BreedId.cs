using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects.Ids;

public class BreedId : ValueObject
{
    private BreedId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static BreedId NewId() => new(Guid.NewGuid());

    public static BreedId Empty() => new(Guid.Empty);

    public static BreedId Create(Guid id) => new(id);

    public static implicit operator BreedId(Guid id) => new(id);

    public static implicit operator Guid(BreedId breedId)
    {
        ArgumentNullException.ThrowIfNull(breedId);

        return breedId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}