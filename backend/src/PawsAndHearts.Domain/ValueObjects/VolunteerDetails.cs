namespace PawsAndHearts.Domain.ValueObjects;

public record VolunteerDetails
{
    private readonly List<SocialNetwork> _socialNetworks = [];

    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

    private readonly List<Requisite> _requisites = [];

    public IReadOnlyList<Requisite> Requisites => _requisites;
}