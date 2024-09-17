using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.AddPhotosToPet;

public record AddPhotosToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files);