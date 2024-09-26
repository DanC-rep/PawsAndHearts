using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;