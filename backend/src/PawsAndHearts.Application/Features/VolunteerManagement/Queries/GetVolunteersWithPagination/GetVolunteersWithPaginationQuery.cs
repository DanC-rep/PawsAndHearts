using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(
    string? SortBy,
    string? SortDirection,
    int Page, 
    int PageSize) : IQuery;