using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdateRequisites;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdateRequisitesRequest(IEnumerable<RequisiteDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, Requisites);
}