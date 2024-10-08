using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetBreedBySpecies;

public record GetBreedBySpeciesQuery(Guid SpeciesId, Guid BreedId) : IQuery;