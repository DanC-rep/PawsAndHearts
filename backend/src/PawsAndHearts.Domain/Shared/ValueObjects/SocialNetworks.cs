namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record SocialNetworks
{
    private SocialNetworks()
    {
        
    }
    
    public SocialNetworks(IEnumerable<SocialNetwork>? socialNetworks)
    {
        Value = socialNetworks?.ToList();
    }
    
    public IReadOnlyList<SocialNetwork>? Value { get; }
}