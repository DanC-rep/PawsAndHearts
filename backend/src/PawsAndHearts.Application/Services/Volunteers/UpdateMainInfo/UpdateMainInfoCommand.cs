using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId, FullNameDto FullName, int Experience, string PhoneNumber);