using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateRequisites;

public record UpdateRequisitesRequest(Guid VolunteerId, UpdateRequisitesDto Dto);

public record UpdateRequisitesDto(IEnumerable<RequisiteDto> Requisites);