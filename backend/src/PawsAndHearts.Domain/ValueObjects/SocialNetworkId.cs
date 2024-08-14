namespace PawsAndHearts.Domain.ValueObjects;

public record SocialNetworkId
{
    private SocialNetworkId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static SocialNetworkId NewId() => new(Guid.NewGuid());

    public static SocialNetworkId Empty() => new(Guid.Empty);

    public static SocialNetworkId Create(Guid id) => new(id);
}