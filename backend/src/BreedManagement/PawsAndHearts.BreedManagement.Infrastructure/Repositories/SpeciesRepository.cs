using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.BreedManagement.Domain.Entities;
using PawsAndHearts.BreedManagement.Infrastructure.DbContexts;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.BreedManagement.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _writeDbContext;

    public SpeciesRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    
    public async Task<Guid> Add(Species species, CancellationToken cancellationToken = default)
    {
        await _writeDbContext.AddAsync(species, cancellationToken);

        return species.Id;
    }

    public async Task<Result<Species, Error>> GetById(
        SpeciesId speciesId, 
        CancellationToken cancellationToken = default)
    {
        var species = await _writeDbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);

        if (species is null)
            return Errors.General.NotFound(speciesId);

        return species;
    }

    public Result<Guid, Error> Delete(Species species, CancellationToken cancellationToken = default)
    {
        _writeDbContext.Species.Remove(species);

        return (Guid)species.Id;
    }
}