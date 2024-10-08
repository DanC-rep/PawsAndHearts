using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;