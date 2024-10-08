using Microsoft.AspNetCore.Http;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.UseCases.AddPhotosToPet;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record AddPhotosToPetRequest(IFormFileCollection Files)
{
    public AddPhotosToPetCommand ToCommand(
        Guid volunteerId, 
        Guid petId, 
        IEnumerable<UploadFileDto> fileDtos) =>
        new(volunteerId, petId, fileDtos);
}