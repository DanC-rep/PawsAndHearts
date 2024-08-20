using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;

namespace PawsAndHearts.Domain.Species.Entities;

public class Species : Entity<SpeciesId>
{
    private Species(SpeciesId id) : base(id)
    {
    }
    
    public string Name { get; private set; }

    private readonly List<Breed> _breeds = [];
    
    public IReadOnlyList<Breed> Breeds => _breeds;
}