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
        if (CheckBreedNameNotExists(breed))
        {
            _breeds.Add(breed);

            return Result.Success<Error>();
        }

        return Errors.General.AlreadyExists("breed", "name", breed.Name);
    }

    private bool CheckBreedNameNotExists(Breed breed) =>
        _breeds.All(b => b.Name != breed.Name);

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