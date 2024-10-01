using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.DeletePetSoft;

public record DeletePetSoftCommand(Guid VolunteerId, Guid PetId) : ICommand;