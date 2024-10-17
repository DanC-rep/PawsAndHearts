using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites) : ICommand
{
    public static UpdateRequisitesCommand Create(Guid volunteerId, UpdateRequisitesRequest request) =>
        new(volunteerId, request.Requisites);
}