using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworksRequest(Guid VolunteerId, UpdateSocialNetworksDto Dto);

public record UpdateSocialNetworksDto(IEnumerable<SocialNetworkDto>? SocialNetworks);