using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.Interfaces;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Domain.Species.Entities;

public class Species : Shared.Entity<SpeciesId>
{
    private Species(SpeciesId id) : base(id)
    {
    }

    public Species(SpeciesId id, string name) : base(id)
    {
        Name = name;
    }
    
    public string Name { get; private set; }

    private readonly List<Breed> _breeds = [];
    
    public IReadOnlyList<Breed> Breeds => _breeds;

    public UnitResult<Error> AddBreed(Breed breed)
    {
        var breedNameNotExistsResult = CheckBreedNameNotExists(breed);

        if (breedNameNotExistsResult.IsFailure)
            return breedNameNotExistsResult.Error;
        
        _breeds.Add(breed);

        return Result.Success<Error>();
    }

    private UnitResult<Error> CheckBreedNameNotExists(Breed breed)
    {
        var foundedBreed = _breeds.FirstOrDefault(b => b.Name == breed.Name);

        if (foundedBreed is null)
            return Result.Success<Error>();

        return Errors.General.AlreadyExists("breed", "name", breed.Name);
    }

    public Result<Breed, Error> GetBreedById(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);

        if (breed is null)
            return Errors.General.NotFound(breedId);

        return breed;
    }

    public void RemoveBreed(Breed breed)
    {
        _breeds.Remove(breed);
    }
}