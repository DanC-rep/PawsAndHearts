namespace PawsAndHearts.PetManagement.Contracts.Requests.Pet;

public record GetPetsWithPaginationRequest(
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize,
    Guid? VolunteerId,
    string? Name,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Color,
    string? City,
    string? Street, 
    int? MinAge,
    int? MaxAge,
    int? MinHeight,
    int? MaxHeight,
    int? MinWeight,
    int? MaxWeight,
    string? HelpStatus,
    bool? IsCastrated,
    bool? IsVaccinated);