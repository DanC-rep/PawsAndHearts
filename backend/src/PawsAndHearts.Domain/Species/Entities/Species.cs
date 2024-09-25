using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.Interfaces;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;

namespace PawsAndHearts.Domain.Species.Entities;

public class Species : Shared.Entity<SpeciesId>, ISoftDeletable
{
    private bool _isDeleted = false;
    
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
        _breeds.Add(breed);

        return Result.Success<Error>();
    }

    public void Delete()
    {
        _isDeleted = true;

        foreach (var breed in _breeds)
        {
            breed.Delete();
        }
    }

    public void Restore()
    {
        _isDeleted = false;

        foreach (var breed in _breeds)
        {
            breed.Restore();
        }
    }

    public Result<Breed, Error> GetBreedById(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);

        if (breed is null)
            return Errors.General.NotFound(breedId);

        return breed;
    }
}