using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.PetManagement.Contracts.Requests.Pet;

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
    bool? IsVaccinated) : IQuery
{
    public static GetPetsWithPaginationQuery Create(GetPetsWithPaginationRequest request) =>
        new(
            request.SortBy,
            request.SortDirection,
            request.Page,
            request.PageSize,
            request.VolunteerId,
            request.Name,
            request.SpeciesId,
            request.BreedId,
            request.Color,
            request.City,
            request.Street,
            request.MinAge,
            request.MaxAge,
            request.MinHeight,
            request.MaxHeight,
            request.MinWeight,
            request.MaxWeight,
            request.HelpStatus,
            request.IsCastrated,
            request.IsVaccinated);
}