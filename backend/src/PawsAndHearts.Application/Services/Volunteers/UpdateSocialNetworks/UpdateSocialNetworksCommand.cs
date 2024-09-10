using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialNetworkDto> SocialNetworks);