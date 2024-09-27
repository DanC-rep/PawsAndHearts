using PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetBreedsBySpecies;

namespace PawsAndHearts.API.Controllers.Species.Requests;

public record GetBreedsBySpeciesRequest(Guid SpeciesId, string? SortDirection)
{
    public GetBreedsBySpeciesQuery ToQuery() => new(SpeciesId, SortDirection);
}