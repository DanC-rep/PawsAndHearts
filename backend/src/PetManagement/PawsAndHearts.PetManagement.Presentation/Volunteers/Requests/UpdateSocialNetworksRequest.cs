using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.UseCases.UpdateSocialNetworks;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialNetworksCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, SocialNetworks);
}