using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.DeleteVolunteer;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;