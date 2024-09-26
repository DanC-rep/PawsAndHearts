using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;