using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.UseCases.DeletePetSoft;

public record DeletePetSoftCommand(Guid VolunteerId, Guid PetId) : ICommand;