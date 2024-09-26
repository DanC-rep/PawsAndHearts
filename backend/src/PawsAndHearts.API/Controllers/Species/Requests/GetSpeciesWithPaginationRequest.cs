using PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetSpeciesWithPagination;

namespace PawsAndHearts.API.Controllers.Species.Requests;

public record GetSpeciesWithPaginationRequest(
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new(SortDirection, Page, PageSize);
}