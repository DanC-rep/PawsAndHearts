using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {
    }
    
    public string Name { get; private set; }
    
    public SpeciesId SpeciesId { get; private set; }

    public Pet Pet { get; private set; }
}