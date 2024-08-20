namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record SocialNetworks
{
    private SocialNetworks()
    {
    }
    
    public SocialNetworks(IEnumerable<SocialNetwork>? socialNetworks)
    {
        _value = socialNetworks?.ToList();
    }
    
    private readonly List<SocialNetwork>? _value;

    public IReadOnlyList<SocialNetwork>? Value => _value;
}