using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery
{
    public static GetVolunteersWithPaginationQuery Create(GetVolunteersWithPaginationRequest request) =>
        new(
            request.SortBy,
            request.SortDirection,
            request.Page,
            request.PageSize);
}