using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites);