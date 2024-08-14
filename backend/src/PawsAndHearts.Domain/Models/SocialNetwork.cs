using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.Models;

public class SocialNetwork : Entity<BaseId>
{
    private SocialNetwork(BaseId id) : base(id)
    {
    }

    public string Link { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public BaseId VolunteerId { get; private set; }
}