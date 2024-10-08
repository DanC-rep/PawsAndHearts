using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.PetManagement.Contracts;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.PetManagement.Presentation;

public class PetManagementContract : IPetManagementContract
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public PetManagementContract(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<UnitResult<Error>> CheckPetsDoNotHaveBreed(
        Guid breedId, 
        CancellationToken cancellationToken = default)
    {
        var pet = await _readDbContext.Pets.
            FirstOrDefaultAsync(p => p.BreedId == breedId, cancellationToken);

        if (pet is null)
            return Result.Success<Error>();

        return Errors.General.AlreadyUsed(breedId);
    }

    public async Task<UnitResult<Error>> CheckPetsDoNotHaveSpecies(
        Guid speciesId, 
        CancellationToken cancellationToken = default)
    {
        var pet = await _readDbContext.Pets
            .FirstOrDefaultAsync(p => p.SpeciesId == speciesId, cancellationToken);

        if (pet is null)
            return Result.Success<Error>();

        return Errors.General.AlreadyUsed(speciesId);
    }
}