using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.BreedManagement.Contracts.Dtos;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetBreedBySpecies;

public class GetBreedBySpeciesHandler : IQueryHandlerWithResult<BreedDto, GetBreedBySpeciesQuery>
{
    private readonly ISpeciesReadDbContext _speciesReadDbContext;

    public GetBreedBySpeciesHandler(ISpeciesReadDbContext speciesReadDbContext)
    {
        _speciesReadDbContext = speciesReadDbContext;
    }

    public async Task<Result<BreedDto, ErrorList>> Handle(
        GetBreedBySpeciesQuery query,
        CancellationToken cancellationToken = default)
    {
        var breedDto = await _speciesReadDbContext.Breeds
            .FirstOrDefaultAsync(b => b.Id == query.BreedId && b.SpeciesId == query.SpeciesId,
                cancellationToken);

        if (breedDto is null)
            return Errors.General.NotFound(query.BreedId).ToErrorList();

        return breedDto;
    }
}