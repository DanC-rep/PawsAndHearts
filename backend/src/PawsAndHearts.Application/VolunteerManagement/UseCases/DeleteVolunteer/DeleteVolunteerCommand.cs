using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.DeleteVolunteer;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;