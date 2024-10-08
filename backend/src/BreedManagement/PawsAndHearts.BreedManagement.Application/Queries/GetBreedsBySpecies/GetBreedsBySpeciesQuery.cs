using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetBreedsBySpecies;

public record GetBreedsBySpeciesQuery(Guid SpeciesId, string? SortDirection) : IQuery;