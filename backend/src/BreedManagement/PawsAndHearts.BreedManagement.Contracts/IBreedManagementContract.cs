using CSharpFunctionalExtensions;
using PawsAndHearts.BreedManagement.Contracts.Dtos;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Contracts;

public interface IBreedManagementContract
{
    Task<Result<SpeciesDto, ErrorList>> GetSpeciesById(
        Guid speciesId, CancellationToken cancellationToken = default);
    
    Task<Result<BreedDto, ErrorList>> GetBreedBySpecies(
        Guid speciesId, Guid breedId, CancellationToken cancellationToken = default);
}