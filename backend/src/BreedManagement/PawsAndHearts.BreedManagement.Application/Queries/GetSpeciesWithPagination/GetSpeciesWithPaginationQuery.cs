using PawsAndHearts.BreedManagement.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(
    string? SortDirection,
    int Page,
    int PageSize) : IQuery
{
    public static GetSpeciesWithPaginationQuery Create(GetSpeciesWithPaginationRequest request) =>
        new(request.SortDirection, request.Page, request.PageSize);
}