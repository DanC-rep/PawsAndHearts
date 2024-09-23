using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.VolunteerManagement.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;