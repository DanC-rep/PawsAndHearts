using PawsAndHearts.PetManagement.Application.UseCases.UpdatePetStatus;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record UpdatePetStatusRequest(string Status)
{
    public UpdatePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, Status);
}