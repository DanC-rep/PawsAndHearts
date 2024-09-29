using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Features.VolunteerManagement.UseCases.AddPhotosToPet;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record AddPhotosToPetRequest(IFormFileCollection Files)
{
    public AddPhotosToPetCommand ToCommand(
        Guid volunteerId, 
        Guid petId, 
        IEnumerable<UploadFileDto> fileDtos) =>
        new(volunteerId, petId, fileDtos);
}