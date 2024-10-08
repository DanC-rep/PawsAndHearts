using PawsAndHearts.BreedManagement.Application.Queries.GetBreedsBySpecies;

namespace PawsAndHearts.BreedManagement.Presentation.Requests;

public record GetBreedsBySpeciesRequest(Guid SpeciesId, string? SortDirection)
{
    public GetBreedsBySpeciesQuery ToQuery() => new(SpeciesId, SortDirection);
}