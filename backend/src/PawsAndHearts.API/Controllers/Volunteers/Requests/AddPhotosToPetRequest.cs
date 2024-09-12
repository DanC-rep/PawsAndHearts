using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Services.Volunteers.AddPhotosToPet;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record AddPhotosToPetRequest(Guid PetId, IFormFileCollection Files)
{
    public AddPhotosToPetCommand ToCommand(Guid volunteerId, IEnumerable<UploadFileDto> fileDtos) =>
        new(volunteerId, PetId, fileDtos);
}