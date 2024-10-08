using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.Queries.GetSpeciesById;

public record GetSpeciesByIdQuery(Guid SpeciesId) : IQuery;