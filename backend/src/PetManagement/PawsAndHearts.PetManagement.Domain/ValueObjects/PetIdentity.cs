using CSharpFunctionalExtensions;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.PetManagement.Domain.ValueObjects;

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