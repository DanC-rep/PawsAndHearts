using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetBreedsBySpecies;

public class GetBreedsBySpeciesHandler : IQueryHandlerWithResult<IEnumerable<BreedDto>, GetBreedsBySpeciesQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetBreedsBySpeciesHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<IEnumerable<BreedDto>, ErrorList>> Handle(
        GetBreedsBySpeciesQuery query,
        CancellationToken cancellationToken = default)
    {
        var species = await _readDbContext.Species
            .FirstOrDefaultAsync(s => s.Id == query.SpeciesId, cancellationToken);

        if (species is null)
            return Errors.General.NotFound(query.SpeciesId).ToErrorList();

        var breedsQuery = _readDbContext.Breeds
            .Where(b => b.SpeciesId == query.SpeciesId);
        
        breedsQuery = query.SortDirection?.ToLower() == "desc"
            ? breedsQuery.OrderByDescending(keySelector => keySelector.Name)
            : breedsQuery.OrderBy(keySelector => keySelector.Name);

        return breedsQuery.ToList();
    }
}