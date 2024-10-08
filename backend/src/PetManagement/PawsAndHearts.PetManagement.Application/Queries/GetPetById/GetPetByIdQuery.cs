using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;