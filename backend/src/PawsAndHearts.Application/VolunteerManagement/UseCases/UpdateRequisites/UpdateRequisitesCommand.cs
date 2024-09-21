using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites) : ICommand;