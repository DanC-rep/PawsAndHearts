using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.UseCases.DeletePetPhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId) : ICommand;