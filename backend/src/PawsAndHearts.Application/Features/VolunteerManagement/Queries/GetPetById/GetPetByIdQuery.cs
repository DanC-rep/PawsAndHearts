using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;