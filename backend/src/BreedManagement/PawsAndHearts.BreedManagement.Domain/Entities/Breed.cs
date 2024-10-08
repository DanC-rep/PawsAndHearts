using CSharpFunctionalExtensions;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.BreedManagement.Domain.Entities;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {
    }

    public Breed(BreedId id, string name, SpeciesId speciesId) : base(id)
    {
        Name = name;
        SpeciesId = speciesId;
    }
    
    public string Name { get; private set; }
    
    public SpeciesId SpeciesId { get; private set; }
}