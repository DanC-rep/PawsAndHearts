using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;

namespace PawsAndHearts.Domain.Species.Entities;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {
    }
    
    public string Name { get; private set; }
    
    public SpeciesId SpeciesId { get; private set; }
}