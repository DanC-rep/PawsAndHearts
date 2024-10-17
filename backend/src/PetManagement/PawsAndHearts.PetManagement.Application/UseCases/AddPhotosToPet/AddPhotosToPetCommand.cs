using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.AddPhotosToPet;

public record AddPhotosToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files) : ICommand
{
    public static AddPhotosToPetCommand Create(
        Guid volunteerId,
        Guid petId,
        IEnumerable<UploadFileDto> files) =>
        new(volunteerId, petId, files);
}