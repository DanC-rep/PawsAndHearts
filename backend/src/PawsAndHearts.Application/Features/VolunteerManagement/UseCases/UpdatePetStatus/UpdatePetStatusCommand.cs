using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string Status) : ICommand;