using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class Volunteer : Entity<VolunteerId>
{
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public FullName FullName { get; private set; }
    
    public int Experience { get; private set; }
    
    public int PetsFoundHome { get; private set; }
    
    public int PetsLookingForHome { get; private set; }
    
    public int PetsBeingTreated { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public VolunteerDetails VolunteerDetails { get; private set; }

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets;
}