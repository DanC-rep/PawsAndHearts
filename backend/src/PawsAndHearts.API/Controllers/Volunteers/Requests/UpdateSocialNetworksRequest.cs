using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks);