using PawsAndHearts.BreedManagement.Application.Interfaces;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.Core.Models;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public GetSpeciesWithPaginationHandler(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<SpeciesDto>> Handle(
        GetSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var speciesQuery = _readDbContext.Species;
        
        speciesQuery = query.SortDirection?.ToLower() == "desc"
            ? speciesQuery.OrderByDescending(keySelector => keySelector.Name)
            : speciesQuery.OrderBy(keySelector => keySelector.Name);

        var result = await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        return result;
    }
}