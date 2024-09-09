using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.AddPhotosToPet;

public record AddPhotosToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files);