using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetBreedsBySpecies;

public class GetBreedsBySpeciesHandler : IQueryHandlerWithResult<IEnumerable<BreedDto>, GetBreedsBySpeciesQuery>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public GetBreedsBySpeciesHandler(ISpeciesReadDbContext readDbContext)
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