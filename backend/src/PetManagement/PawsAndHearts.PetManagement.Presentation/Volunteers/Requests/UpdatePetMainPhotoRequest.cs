using PawsAndHearts.PetManagement.Application.UseCases.UpdatePetMainPhoto;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record UpdatePetMainPhotoRequest(string FilePath)
{
    public UpdatePetMainPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(FilePath, volunteerId, petId);
}