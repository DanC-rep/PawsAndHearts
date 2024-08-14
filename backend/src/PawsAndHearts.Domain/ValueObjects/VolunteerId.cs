using PawsAndHearts.Domain.Models;

namespace PawsAndHearts.Domain.ValueObjects;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static VolunteerId NewId() => new(Guid.NewGuid());

    public static VolunteerId Empty() => new(Guid.Empty);

    public static VolunteerId Create(Guid id) => new(id);
}