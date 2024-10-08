using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.UseCases.CreateVolunteer;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

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