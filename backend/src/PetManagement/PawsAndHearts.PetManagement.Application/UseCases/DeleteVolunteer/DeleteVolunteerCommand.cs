using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.PetManagement.Application.UseCases.DeleteVolunteer;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;