namespace PawsAndHearts.Domain.ValueObjects;

public record RequisiteId
{
    private RequisiteId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static RequisiteId NewId() => new(Guid.NewGuid());

    public static RequisiteId Empty() => new(Guid.Empty);

    public static RequisiteId Create(Guid id) => new(id);
}