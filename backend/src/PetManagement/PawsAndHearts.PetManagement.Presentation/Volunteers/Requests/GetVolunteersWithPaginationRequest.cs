using PawsAndHearts.PetManagement.Application.Queries.GetVolunteersWithPagination;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record GetVolunteersWithPaginationRequest(
    string? SortBy,
    string? SortDirection,
    int Page, 
    int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new (SortBy, SortDirection, Page, PageSize);
}