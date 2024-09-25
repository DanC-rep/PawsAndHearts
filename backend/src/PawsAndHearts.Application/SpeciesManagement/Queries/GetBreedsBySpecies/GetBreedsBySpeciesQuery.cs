using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.SpeciesManagement.Queries.GetBreedsBySpecies;

public record GetBreedsBySpeciesQuery(Guid SpeciesId, string? SortDirection) : IQuery;