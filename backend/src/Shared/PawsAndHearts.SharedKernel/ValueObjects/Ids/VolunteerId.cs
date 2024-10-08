using CSharpFunctionalExtensions;

namespace PawsAndHearts.SharedKernel.ValueObjects.Ids;

public class VolunteerId : ValueObject
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static VolunteerId NewId() => new(Guid.NewGuid());

    public static VolunteerId Empty() => new(Guid.Empty);

    public static VolunteerId Create(Guid id) => new(id);

    public static implicit operator VolunteerId(Guid id) => new(id);

    public static implicit operator Guid(VolunteerId volunteerId)
    {
        ArgumentNullException.ThrowIfNull(volunteerId);

        return volunteerId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}