namespace PawsAndHearts.BreedManagement.Contracts.Requests;

public record GetSpeciesWithPaginationRequest(
    string? SortDirection,
    int Page,
    int PageSize);