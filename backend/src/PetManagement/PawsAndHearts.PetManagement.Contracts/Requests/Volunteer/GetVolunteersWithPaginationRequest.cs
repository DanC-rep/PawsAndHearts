namespace PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

public record GetVolunteersWithPaginationRequest(
    string? SortBy,
    string? SortDirection,
    int Page, 
    int PageSize);