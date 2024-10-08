using PawsAndHearts.BreedManagement.Application.Queries.GetSpeciesWithPagination;

namespace PawsAndHearts.BreedManagement.Presentation.Requests;

public record GetSpeciesWithPaginationRequest(
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new(SortDirection, Page, PageSize);
}