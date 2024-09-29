using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.DeletePetPhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId) : ICommand;