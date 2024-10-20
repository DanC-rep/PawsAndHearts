using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.Accounts.Contracts.Requests;

public record RegisterUserRequest(
    string Email, 
    string UserName, 
    FullNameDto FullName,
    IReadOnlyList<SocialNetworkDto> SocialNetworks,
    string Password);