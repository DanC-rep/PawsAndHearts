using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;