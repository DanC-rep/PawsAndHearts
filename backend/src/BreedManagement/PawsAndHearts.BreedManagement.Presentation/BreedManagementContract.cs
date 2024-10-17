using CSharpFunctionalExtensions;
using PawsAndHearts.BreedManagement.Application.Queries.GetBreedBySpecies;
using PawsAndHearts.BreedManagement.Application.Queries.GetSpeciesById;
using PawsAndHearts.BreedManagement.Contracts;
using PawsAndHearts.BreedManagement.Contracts.Dtos;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Presentation;

public class BreedManagementContract : IBreedManagementContract
{
    private readonly GetSpeciesByIdHandler _getSpeciesByIdHandler;
    private readonly GetBreedBySpeciesHandler _getBreedBySpeciesHandler;

    public BreedManagementContract(
        GetSpeciesByIdHandler getSpeciesByIdHandler,
        GetBreedBySpeciesHandler getBreedBySpeciesHandler)
    {
        _getSpeciesByIdHandler = getSpeciesByIdHandler;
        _getBreedBySpeciesHandler = getBreedBySpeciesHandler;
    }
    
    public async Task<Result<SpeciesDto, ErrorList>> GetSpeciesById(
        Guid speciesId, 
        CancellationToken cancellationToken = default)
    {
        var query = new GetSpeciesByIdQuery(speciesId);
        
        return await _getSpeciesByIdHandler.Handle(query, cancellationToken);
    }

    public async Task<Result<BreedDto, ErrorList>> GetBreedBySpecies(
        Guid speciesId, 
        Guid breedId, 
        CancellationToken cancellationToken = default)
    {
        var query = new GetBreedBySpeciesQuery(speciesId, breedId);

        return await _getBreedBySpeciesHandler.Handle(query, cancellationToken);
    }
}