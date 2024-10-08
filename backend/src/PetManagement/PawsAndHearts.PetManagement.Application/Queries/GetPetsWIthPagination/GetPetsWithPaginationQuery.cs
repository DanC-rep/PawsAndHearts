using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.Queries.GetPetsWIthPagination;

public record GetPetsWithPaginationQuery(
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
    bool? IsNeutered,
    bool? IsVaccinated) : IQuery;