using PawsAndHearts.BreedManagement.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetBreedsBySpecies;

public record GetBreedsBySpeciesQuery(Guid SpeciesId, string? SortDirection) : IQuery
{
    public static GetBreedsBySpeciesQuery Create(GetBreedsBySpeciesRequest request) =>
        new(request.SpeciesId, request.SortDirection);
}