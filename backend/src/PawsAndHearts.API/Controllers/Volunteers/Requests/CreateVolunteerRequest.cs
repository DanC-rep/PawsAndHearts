using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    int Experience,
    string PhoneNumber,
    IEnumerable<RequisiteDto> Requisites,
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public CreateVolunteerCommand ToCommand() => 
        new CreateVolunteerCommand(
            FullName,
            Experience,
            PhoneNumber,
            Requisites,
            SocialNetworks);
}