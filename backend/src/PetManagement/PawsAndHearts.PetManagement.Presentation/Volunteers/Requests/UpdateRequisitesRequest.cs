using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.UseCases.UpdateRequisites;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record UpdateRequisitesRequest(IEnumerable<RequisiteDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, Requisites);
}