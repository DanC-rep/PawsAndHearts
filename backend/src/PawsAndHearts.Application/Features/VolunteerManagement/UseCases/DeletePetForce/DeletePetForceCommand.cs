using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.DeletePetForce;

public record DeletePetForceCommand(Guid VolunteerId, Guid PetId) : ICommand;