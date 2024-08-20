using PawsAndHearts.Domain.Shared.ValueObjects.Ids;

namespace PawsAndHearts.Domain.Volunteer.ValueObjects;

public record PetIdentity
{
    public PetIdentity(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public SpeciesId SpeciesId { get; } = default!;

    public Guid BreedId { get; } = default!;
    
}