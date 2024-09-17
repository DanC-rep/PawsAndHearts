using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialNetworkDto> SocialNetworks);