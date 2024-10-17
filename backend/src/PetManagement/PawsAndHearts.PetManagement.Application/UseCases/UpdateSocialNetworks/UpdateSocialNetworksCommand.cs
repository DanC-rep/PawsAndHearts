using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand
{
    public static UpdateSocialNetworksCommand Create(
        Guid volunteerId, 
        UpdateSocialNetworksRequest request) =>
        new(volunteerId, request.SocialNetworks);
}