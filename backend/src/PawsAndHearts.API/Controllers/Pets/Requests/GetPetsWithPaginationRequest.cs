using PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetPetsWIthPagination;

namespace PawsAndHearts.API.Controllers.Pets.Requests;

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
    bool? IsVaccinated)
{
    public GetPetsWithPaginationQuery ToQuery() =>
        new(SortBy, 
            SortDirection, 
            Page, 
            PageSize,
            VolunteerId,
            Name,
            SpeciesId,
            BreedId,
            Color,
            City,
            Street,
            MinAge,
            MaxAge,
            MinHeight,
            MaxHeight,
            MinWeight,
            MaxWeight,
            HelpStatus,
            IsCastrated,
            IsVaccinated);
}