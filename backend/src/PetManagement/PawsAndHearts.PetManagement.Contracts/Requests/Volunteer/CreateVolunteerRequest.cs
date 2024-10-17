using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    int Experience,
    string PhoneNumber,
    IEnumerable<RequisiteDto> Requisites,
    IEnumerable<SocialNetworkDto> SocialNetworks);