namespace PawsAndHearts.Domain.Models;

public class SocialNetwork
{
    public Guid Id { get; private set; }

    public string Link { get; private set; } = default!;

    public string Name { get; private set; } = default!;
}