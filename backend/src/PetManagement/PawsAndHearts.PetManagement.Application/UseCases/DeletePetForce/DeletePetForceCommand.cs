using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.UseCases.DeletePetForce;

public record DeletePetForceCommand(Guid VolunteerId, Guid PetId) : ICommand;