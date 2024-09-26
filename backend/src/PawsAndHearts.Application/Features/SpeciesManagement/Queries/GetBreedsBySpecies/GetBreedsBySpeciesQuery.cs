using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetBreedsBySpecies;

public record GetBreedsBySpeciesQuery(Guid SpeciesId, string? SortDirection) : IQuery;