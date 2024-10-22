using PawsAndHearts.Accounts.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.Accounts.Application.UseCases.UpdateUserSocialNetworks;

public record UpdateUserSocialNetworksCommand(
    IEnumerable<SocialNetworkDto> SocialNetworks, Guid UserId) : ICommand
{
    public static UpdateUserSocialNetworksCommand Create(
        UpdateUserSocialNetworksRequest request, Guid userId) =>
        new(request.SocialNetworks, userId);
}