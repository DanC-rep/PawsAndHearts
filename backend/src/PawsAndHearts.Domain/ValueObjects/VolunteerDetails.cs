using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.ValueObjects;

public record VolunteerDetails
{
    private VolunteerDetails()
    {
    }
    
    private VolunteerDetails(List<SocialNetwork>? socialNetworks, List<Requisite>? requisites)
    {
        _socialNetworks = socialNetworks;
        _requisites = requisites;
    }
    
    private readonly List<SocialNetwork>? _socialNetworks = [];

    public IReadOnlyList<SocialNetwork>? SocialNetworks => _socialNetworks;

    private readonly List<Requisite>? _requisites = [];

    public IReadOnlyList<Requisite>? Requisites => _requisites;

    public static Result<VolunteerDetails, Error> Create(
        List<SocialNetwork>? socialNetworks, 
        List<Requisite>? requisites)
    {
        return new VolunteerDetails(socialNetworks, requisites);
    }
}