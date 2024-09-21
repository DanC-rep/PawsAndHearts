using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;