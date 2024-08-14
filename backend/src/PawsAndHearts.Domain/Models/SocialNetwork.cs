using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class SocialNetwork : Entity<SocialNetworkId>
{
    private SocialNetwork(SocialNetworkId id) : base(id)
    {
    }

    public string Link { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public VolunteerId VolunteerId { get; private set; }
}