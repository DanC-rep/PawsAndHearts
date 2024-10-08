using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdatePetMainPhoto;

public record UpdatePetMainPhotoCommand(string FilePath, Guid VolunteerId, Guid PetId) : ICommand;