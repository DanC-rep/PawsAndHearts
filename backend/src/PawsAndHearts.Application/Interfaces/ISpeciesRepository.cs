using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Species.Entities;

namespace PawsAndHearts.Application.Interfaces;

public interface ISpeciesRepository
{
    Task<Guid> Add(Species species, CancellationToken cancellationToken = default);
    
    Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken = default);
}