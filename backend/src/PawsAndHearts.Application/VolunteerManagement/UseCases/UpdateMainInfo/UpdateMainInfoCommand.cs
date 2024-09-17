using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId, FullNameDto FullName, int Experience, string PhoneNumber);