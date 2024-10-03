using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetMainPhoto;

public record UpdatePetMainPhotoCommand(string FilePath, Guid VolunteerId, Guid PetId) : ICommand;