using PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetStatus;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdatePetStatusRequest(string Status)
{
    public UpdatePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, Status);
}