using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Application.Models;

namespace PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetSpeciesWithPaginationHandler> _logger;

    public GetSpeciesWithPaginationHandler(
        IReadDbContext readDbContext,
        ILogger<GetSpeciesWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
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
        
        _logger.LogInformation("Species was received with count: {totalCount}", result.Items.Count);

        return result;
    }
}