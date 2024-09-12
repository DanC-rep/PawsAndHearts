using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Services.Volunteers.UpdateRequisites;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdateRequisitesRequest(IEnumerable<RequisiteDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, Requisites);
}