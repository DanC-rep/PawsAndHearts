using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;

namespace PawsAndHearts.Domain.Volunteer.ValueObjects;

public class PetIdentity : ValueObject
{
    public PetIdentity(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public SpeciesId SpeciesId { get; } = default!;

    public Guid BreedId { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return SpeciesId;
        yield return BreedId;
    }
}