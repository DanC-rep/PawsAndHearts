using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;