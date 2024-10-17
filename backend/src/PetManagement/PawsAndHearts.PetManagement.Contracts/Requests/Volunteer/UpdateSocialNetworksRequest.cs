using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks);