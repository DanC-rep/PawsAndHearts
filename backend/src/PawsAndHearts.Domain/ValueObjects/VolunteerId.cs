using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.ValueObjects;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static Result<VolunteerId> NewId() => new VolunteerId(Guid.NewGuid());

    public static Result<VolunteerId> Empty() => new VolunteerId(Guid.Empty);

    public static Result<VolunteerId> Create(Guid id) => new VolunteerId(id);
}