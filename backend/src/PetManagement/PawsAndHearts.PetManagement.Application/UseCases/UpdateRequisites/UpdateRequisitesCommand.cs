using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites) : ICommand;