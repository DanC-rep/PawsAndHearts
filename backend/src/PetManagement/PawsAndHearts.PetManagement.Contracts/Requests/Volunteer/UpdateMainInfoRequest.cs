using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

public record UpdateMainInfoRequest(FullNameDto FullName, int Experience, string PhoneNumber);