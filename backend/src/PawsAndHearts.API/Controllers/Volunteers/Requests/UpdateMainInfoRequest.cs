using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdateMainInfoRequest(FullNameDto FullName, int Experience, string PhoneNumber);