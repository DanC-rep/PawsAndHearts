using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Application.UseCases.AddPhotosToPet;

public record AddPhotosToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files) : ICommand;