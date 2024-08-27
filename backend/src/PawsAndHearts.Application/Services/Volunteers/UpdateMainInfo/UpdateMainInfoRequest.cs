using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateMainInfo;

public record UpdateMainInfoRequest(Guid VolunteerId, UpdateMainInfoDto Dto);

public record UpdateMainInfoDto(FullNameDto FullName, int Experience, string PhoneNumber);