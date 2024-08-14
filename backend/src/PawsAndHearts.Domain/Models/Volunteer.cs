using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class Volunteer : Entity<VolunteerId>
{
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public string Name { get; private set; } = default!;

    public string Surname { get; private set; } = default!;
    
    public string? Patronymic { get; private set; }
    
    public int Experience { get; private set; }
    
    public int PetsFoundHome { get; private set; }
    
    public int PetsLookingForHome { get; private set; }
    
    public int PetsBeingTreated { get; private set; }

    public string Phone { get; private set; } = default!;

    private readonly List<SocialNetwork> _socialNetworks = [];

    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

    private readonly List<Requisite> _requisites = [];

    public IReadOnlyList<Requisite> Requisites => _requisites;

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets;
}