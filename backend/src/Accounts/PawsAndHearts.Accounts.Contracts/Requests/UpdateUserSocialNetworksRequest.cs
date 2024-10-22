using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.Accounts.Contracts.Requests;

public record UpdateUserSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks);