using PawsAndHearts.Accounts.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.Accounts.Application.UseCases.Register;

public record RegisterUserCommand(
    string Email, 
    string UserName, 
    FullNameDto FullName,
    IReadOnlyList<SocialNetworkDto> SocialNetworks,
    string Password) : ICommand
{
    public static RegisterUserCommand Create(RegisterUserRequest request) =>
        new(request.Email,
            request.UserName,
            request.FullName,
            request.SocialNetworks,
            request.Password);
}