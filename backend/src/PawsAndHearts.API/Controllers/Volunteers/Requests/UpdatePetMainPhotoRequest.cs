using PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetMainPhoto;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdatePetMainPhotoRequest(string FilePath)
{
    public UpdatePetMainPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(FilePath, volunteerId, petId);
}