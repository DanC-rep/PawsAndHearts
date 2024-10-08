using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(
    string? SortBy,
    string? SortDirection,
    int Page, 
    int PageSize) : IQuery;