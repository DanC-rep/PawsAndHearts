using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    FullNameDto FullName, 
    int Experience,
    string PhoneNumber, 
    IEnumerable<RequisiteDto>? Requisites, 
    IEnumerable<SocialNetworkDto>? SocialNetworks);