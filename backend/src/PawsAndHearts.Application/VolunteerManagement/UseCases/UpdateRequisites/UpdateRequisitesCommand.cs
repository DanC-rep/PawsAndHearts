using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites);