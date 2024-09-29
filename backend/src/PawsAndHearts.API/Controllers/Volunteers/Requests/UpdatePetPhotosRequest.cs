using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetPhotos;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdatePetPhotosRequest(IFormFileCollection Files)
{
    public UpdatePetPhotosCommand ToCommand(
        Guid volunteerId,
        Guid petId,
        IEnumerable<UploadFileDto> fileDtos) =>
        new(volunteerId, petId, fileDtos);
}