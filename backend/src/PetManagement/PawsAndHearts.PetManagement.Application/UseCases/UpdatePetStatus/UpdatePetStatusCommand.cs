using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string Status) : ICommand
{
    public static UpdatePetStatusCommand Create(
        Guid volunteerId, Guid petId, UpdatePetStatusRequest request) =>
        new(volunteerId, petId, request.Status);
}