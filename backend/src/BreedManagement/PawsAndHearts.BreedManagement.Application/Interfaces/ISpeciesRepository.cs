using CSharpFunctionalExtensions;
using PawsAndHearts.BreedManagement.Domain.Entities;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.BreedManagement.Application.Interfaces;

public interface ISpeciesRepository
{
    Task<Guid> Add(Species species, CancellationToken cancellationToken = default);
    
    Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken = default);
    
    Result<Guid, Error> Delete(Species species, CancellationToken cancellationToken = default);
}